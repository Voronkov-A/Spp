using System;
using System.Globalization;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Fluid;
using Fluid.Values;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Values;

public class JsonObjectValue(JsonObject? inner) : FluidValue
{
    private readonly JsonObject? _inner = inner;

    public override FluidValues Type => FluidValues.Object;

    public override void WriteTo(TextWriter writer, TextEncoder encoder, CultureInfo cultureInfo)
    {
        writer.Write(encoder.Encode(ToStringValue()));
    }

    public override bool Equals(FluidValue? other)
    {
        if (other is null)
        {
            return !ToBooleanValue();
        }

        if (other is JsonObjectValue jsonObjectValue)
        {
            return JsonNode.DeepEquals(_inner, jsonObjectValue._inner);
        }

        try
        {
            var obj = other.ToObjectValue();

            if (obj is null)
            {
                return !ToBooleanValue();
            }

            var node = JsonSerializer.SerializeToNode(other.ToObjectValue());
            return JsonNode.DeepEquals(_inner, node);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public override bool Equals(object? obj)
    {
        return obj == null ? !ToBooleanValue() : obj is FluidValue && Equals(obj);
    }

    public override int GetHashCode()
    {
        return _inner?.GetHashCode() ?? 0;
    }

    public override bool ToBooleanValue()
    {
        return _inner is not null;
    }

    public override decimal ToNumberValue()
    {
        return 0;
    }

    public override string ToStringValue()
    {
        return JsonSerializer.Serialize(_inner);
    }

    public override object? ToObjectValue()
    {
        return _inner;
    }

    public override ValueTask<FluidValue> GetValueAsync(string name, TemplateContext context)
    {
        if (name == "this")
        {
            return this;
        }

        if (name.StartsWith("this."))
        {
            name = name["this.".Length..];
        }

        var result = GetPropertyValue(name, context);
        return ValueTask.FromResult(result);
    }

    public override ValueTask<FluidValue> GetIndexAsync(FluidValue index, TemplateContext context)
    {
        var result = GetPropertyValue(index.ToStringValue(), context);
        return ValueTask.FromResult(result);
    }

    private FluidValue GetPropertyValue(string name, TemplateContext context)
    {
        if (_inner == null)
        {
            return NilValue.Instance;
        }

        var tokens = name.Split('.');
        JsonNode? current = _inner;
        var tokenIndex = 0;

        while (tokenIndex < tokens.Length && current is not null)
        {
            current = current[tokens[tokenIndex++]];
        }

        return Create(current, context.Options);
    }
}
