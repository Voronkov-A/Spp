using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Spp.Common.Configuration;

internal static class SettingsValidator
{
    public static bool Validate(object value, ICollection<ValidationResult> validationResults)
    {
        var initialCount = validationResults.Count;
        Validate(new NullabilityInfoContext(), value, "", validationResults);
        return validationResults.Count == initialCount;
    }

    private static void Validate(
        NullabilityInfoContext context,
        object value,
        string path,
        ICollection<ValidationResult> validationResults)
    {
        if (IsPrimitive(value))
        {
            return;
        }

        if (value is IEnumerable collection)
        {
            ValidateCollection(context, collection, path, validationResults);
        }
        else
        {
            ValidateObject(context, value, path, validationResults);
        }
    }

    private static void ValidateCollection(
        NullabilityInfoContext context,
        IEnumerable value,
        string path,
        ICollection<ValidationResult> validationResults)
    {
        var index = 0;

        foreach (var item in value)
        {
            Validate(context, item, AppendPath(path, index++), validationResults);
        }
    }

    private static void ValidateObject(
        NullabilityInfoContext context,
        object value,
        string path,
        ICollection<ValidationResult> validationResults)
    {
        var bindingFlags = BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public;
        ValidateMembers(
            context,
            value,
            path,
            validationResults,
            value.GetType().GetProperties(bindingFlags),
            static (m, v) => m.GetValue(v));
        ValidateMembers(
            context,
            value,
            path,
            validationResults,
            value.GetType().GetFields(bindingFlags),
            static (m, v) => m.GetValue(v));
    }

    private static void ValidateMembers<T>(
        NullabilityInfoContext context,
        object value,
        string path,
        ICollection<ValidationResult> validationResults,
        IEnumerable<T> members,
        Func<T, object, object?> getValue)
        where T : MemberInfo
    {
        foreach (var member in members)
        {
            var memberValue = getValue(member, value);

            if (memberValue == null)
            {
                if (!IsNullable(context, member))
                {
                    validationResults.Add(new ValidationResult(
                        $"The {AppendPath(path, member.Name)} field is required"));
                }
            }
            else
            {
                Validate(context, memberValue, AppendPath(path, member.Name), validationResults);
            }
        }
    }

    private static string AppendPath(string path, int index)
    {
        return $"{path}[{index}]";
    }

    private static string AppendPath(string path, string propertyName)
    {
        return path == "" ? propertyName : $"{path}.{propertyName}";
    }

    private static bool IsPrimitive(object value)
    {
        return value.GetType().IsPrimitive
            || value is string
            || value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    private static bool IsNullable(NullabilityInfoContext context, MemberInfo member)
    {
        return member switch
        {
            PropertyInfo propertyInfo => context.Create(propertyInfo).WriteState == NullabilityState.Nullable,
            FieldInfo fieldInfo => context.Create(fieldInfo).WriteState == NullabilityState.Nullable,
            _ => throw new NotSupportedException($"{member.GetType()} is not supported.")
        };
    }
}
