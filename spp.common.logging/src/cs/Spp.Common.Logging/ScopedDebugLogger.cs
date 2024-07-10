using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Spp.Common.Miscellaneous;
using System;
using System.Diagnostics;
using System.IO;

namespace Spp.Common.Logging;

public partial class ScopedDebugLogger(string name) : ILogger
{
    private const string LoglevelPadding = ": ";

    private static readonly string _messagePadding = new(
        ' ',
        EnumSerializer.ToString(LogLevel.Information).Length + LoglevelPadding.Length);
    [ThreadStatic]
    private static StringWriter? _stringWriter;

    private readonly string _name = name;

    public IExternalScopeProvider? ScopeProvider { get; set; }

    public bool IncludeScopes { get; set; }

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        return ScopeProvider?.Push(state) ?? NullDisposable.Instance;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return Debugger.IsAttached && logLevel != LogLevel.None;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        ArgumentNullException.ThrowIfNull(formatter);

        _stringWriter ??= new StringWriter();
        var logEntry = new LogEntry<TState>(logLevel, _name, eventId, state, exception, formatter);
        Write(in logEntry, ScopeProvider, _stringWriter);

        var sb = _stringWriter.GetStringBuilder();
        if (sb.Length == 0)
        {
            return;
        }
        var computedAnsiString = sb.ToString();
        sb.Clear();
        if (sb.Capacity > 1024)
        {
            sb.Capacity = 1024;
        }

        DebugWriteLine(computedAnsiString, category: _name);
    }

    private void Write<TState>(
        in LogEntry<TState> logEntry,
        IExternalScopeProvider? scopeProvider,
        TextWriter textWriter)
    {
        var message = logEntry.Formatter(logEntry.State, logEntry.Exception);

        if (logEntry.Exception == null && message == null)
        {
            return;
        }

        // We extract most of the work into a non-generic method to save code size. If this was left in the generic
        // method, we'd get generic specialization for all TState parameters, but that's unnecessary.
        WriteInternal(
            scopeProvider,
            textWriter,
            message,
            logEntry.LogLevel,
            logEntry.EventId.Id,
            logEntry.Exception,
            logEntry.Category);
    }

    private void WriteInternal(
        IExternalScopeProvider? scopeProvider,
        TextWriter textWriter,
        string message,
        LogLevel logLevel,
        int eventId,
        Exception? exception,
        string category)
    {
        var logLevelString = EnumSerializer.ToString(logLevel);

        if (logLevelString != null)
        {
            textWriter.Write(logLevelString);
        }

        // Example:
        // info: ConsoleApp.Program[10]
        //       Request received

        // category and event id
        textWriter.Write(LoglevelPadding);
        textWriter.Write(category);
        textWriter.Write('[');

        Span<char> span = stackalloc char[10];
        if (eventId.TryFormat(span, out int charsWritten))
        {
            textWriter.Write(span.Slice(0, charsWritten));
        }
        else
        {
            textWriter.Write(eventId.ToString());
        }

        textWriter.Write(']');

        if (IncludeScopes)
        {
            WriteScopeInformation(textWriter, scopeProvider);
        }

        WriteMessage(textWriter, message);

        // Example:
        // System.InvalidOperationException
        //    at Namespace.Class.Function() in File:line X
        if (exception != null)
        {
            // exception message
            WriteMessage(textWriter, exception.ToString());
        }

        textWriter.Write(Environment.NewLine);
    }

    private static void WriteScopeInformation(TextWriter textWriter, IExternalScopeProvider? scopeProvider)
    {
        if (scopeProvider != null)
        {
            scopeProvider.ForEachScope(
                (scope, state) =>
                {
                    state.Write(" => ");
                    state.Write(scope);
                },
                textWriter);
        }
    }

    private static void WriteMessage(TextWriter textWriter, string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            textWriter.Write(' ');
            WriteReplacing(textWriter, Environment.NewLine, " ", message);
        }

        static void WriteReplacing(TextWriter writer, string oldValue, string newValue, string message)
        {
            string newMessage = message.Replace(oldValue, newValue);
            writer.Write(newMessage);
        }
    }

    private class NullDisposable : IDisposable
    {
        public static readonly NullDisposable Instance = new();

        public void Dispose()
        {
            // pass
        }
    }
}
