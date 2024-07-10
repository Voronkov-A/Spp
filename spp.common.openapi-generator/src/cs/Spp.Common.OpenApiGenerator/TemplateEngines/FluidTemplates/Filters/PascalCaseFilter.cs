using System;
using System.Threading.Tasks;
using Fluid;
using Fluid.Values;

namespace Spp.Common.OpenApiGenerator.TemplateEngines.FluidTemplates.Filters;

public class PascalCaseFilter : IFilter
{
    public string Name => "pascal_case";

    public ValueTask<FluidValue> EvaluateAsync(
        FluidValue input,
        FilterArguments arguments,
        TemplateContext context)
    {
        var result = ToPascalCase(input.ToStringValue());
        return ValueTask.FromResult(FluidValue.Create(result, context.Options));
    }

    private static string ToPascalCase(string value)
    {
        var charArray = new char[value.Length];
        var charCount = 0;
        var nextCase = NextCase.Upper;

        for (var i = 0; i < value.Length; ++i)
        {
            var c = value[i];

            if (c is ' ' or '_')
            {
                nextCase = NextCase.Upper;
                continue;
            }

            switch (nextCase)
            {
                case NextCase.Lower:
                {
                    charArray[charCount++] = char.IsLower(c) ? c : char.ToLowerInvariant(c);
                    nextCase = NextCase.Undefined;
                    break;
                }
                case NextCase.Upper:
                {
                    charArray[charCount++] = char.IsUpper(c) ? c : char.ToUpperInvariant(c);
                    nextCase = NextCase.Lower;
                    break;
                }
                default:
                {
                    charArray[charCount++] = c;
                    break;
                }
            }
        }

        return new string(Shrink(charArray, charCount));
    }

    private static char[] Shrink(char[] source, int length)
    {
        if (source.Length <= length)
        {
            return source;
        }

        var result = new char[length];
        Array.Copy(source, result, length);
        return result;
    }

    private enum NextCase
    {
        Undefined,
        Lower,
        Upper
    }
}
