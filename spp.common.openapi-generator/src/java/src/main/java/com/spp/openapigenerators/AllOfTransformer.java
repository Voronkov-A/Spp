package com.spp.openapigenerators;

import org.openapitools.codegen.CodegenProperty;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class AllOfTransformer {
    public static void transform(final CodegenProperty property) {
        if (property.getComposedSchemas() != null && property.getComposedSchemas().getAllOf() != null) {
            final List<CodegenProperty> allOf = property.getComposedSchemas().getAllOf();
            for (final CodegenProperty composed : allOf) {
                merge(property, composed);
            }
        }
    }

    private static void merge(CodegenProperty left, CodegenProperty right)
    {
        left.openApiType = mergeTypes("openApiType", left.openApiType, right.openApiType);
        left.baseName = takeLeftIfNotNull("baseName", left.baseName, right.baseName);
        left.complexType = mergeTypes("complexType", left.complexType, right.complexType);
        left.getter = takeLeftIfNotNull("getter", left.getter, right.getter);
        left.setter = takeLeftIfNotNull("setter", left.setter, right.setter);
        left.description = takeLeftIfNotNull("description", left.description, right.description);
        left.dataType = mergeTypes("dataType", left.dataType, right.dataType);
        left.datatypeWithEnum = mergeTypes("datatypeWithEnum", left.datatypeWithEnum, right.datatypeWithEnum);
        left.dataFormat = throwIfNotEqual("dataFormat", left.dataFormat, right.dataFormat);
        left.name = takeLeftIfNotNull("name", left.name, right.name);
        left.min = throwIfNotEqual("min", left.min, right.min);
        left.max = throwIfNotEqual("max", left.max, right.max);
        left.defaultValue = throwIfNotEqual("defaultValue", left.defaultValue, right.defaultValue);
        left.defaultValueWithParam = takeLeftIfNotNull("defaultValueWithParam", left.defaultValueWithParam, right.defaultValueWithParam);
        left.baseType = mergeTypes("baseType", left.baseType, right.baseType);
        left.containerType = throwIfNotEqual("containerType", left.containerType, right.containerType);
        left.containerTypeMapped = throwIfNotEqual("containerTypeMapped", left.containerTypeMapped, right.containerTypeMapped);
        left.title = throwIfNotEqual("title", left.title, right.title);
        left.unescapedDescription = takeLeftIfNotNull("unescapedDescription", left.unescapedDescription, right.unescapedDescription);
        left.maxLength = throwIfNotEqual("maxLength", left.maxLength, right.maxLength);
        left.minLength = throwIfNotEqual("minLength", left.minLength, right.minLength);
        left.pattern = throwIfNotEqual("pattern", left.pattern, right.pattern);
        left.example = throwIfNotEqual("example", left.example, right.example);
        left.minimum = throwIfNotEqual("minimum", left.minimum, right.minimum);
        left.maximum = throwIfNotEqual("maximum", left.maximum, right.maximum);
        left.multipleOf = throwIfNotEqual("multipleOf", left.multipleOf, right.multipleOf);
        left.exclusiveMinimum = throwIfNotEqual("exclusiveMinimum", left.exclusiveMinimum, right.exclusiveMinimum);
        left.exclusiveMaximum = throwIfNotEqual("exclusiveMaximum", left.exclusiveMaximum, right.exclusiveMaximum);
        left.required = left.required || right.required;
        left.deprecated = throwIfNotEqual("deprecated", left.deprecated, right.deprecated);
        left.hasMoreNonReadOnly = throwIfNotEqual("hasMoreNonReadOnly", left.hasMoreNonReadOnly, right.hasMoreNonReadOnly);
        left.isPrimitiveType = throwIfNotEqual("isPrimitiveType", left.isPrimitiveType, right.isPrimitiveType);
        left.isModel = left.isModel || right.isModel;
        left.isContainer = throwIfNotEqual("isContainer", left.isContainer, right.isContainer);
        left.isString = throwIfNotEqual("isString", left.isString, right.isString);
        left.isNumeric = throwIfNotEqual("isNumeric", left.isNumeric, right.isNumeric);
        left.isInteger = throwIfNotEqual("isInteger", left.isInteger, right.isInteger);
        left.isShort = throwIfNotEqual("isShort", left.isShort, right.isShort);
        left.isLong = throwIfNotEqual("isLong", left.isLong, right.isLong);
        left.isUnboundedInteger = throwIfNotEqual("isUnboundedInteger", left.isUnboundedInteger, right.isUnboundedInteger);
        left.isNumber = throwIfNotEqual("isNumber", left.isNumber, right.isNumber);
        left.isFloat = throwIfNotEqual("isFloat", left.isFloat, right.isFloat);
        left.isDouble = throwIfNotEqual("isDouble", left.isDouble, right.isDouble);
        left.isDecimal = throwIfNotEqual("isDecimal", left.isDecimal, right.isDecimal);
        left.isByteArray = throwIfNotEqual("isByteArray", left.isByteArray, right.isByteArray);
        left.isBinary = throwIfNotEqual("isBinary", left.isBinary, right.isBinary);
        left.isFile = throwIfNotEqual("isFile", left.isFile, right.isFile);
        left.isBoolean = throwIfNotEqual("isBoolean", left.isBoolean, right.isBoolean);
        left.isDate = throwIfNotEqual("isDate", left.isDate, right.isDate);
        left.isDateTime = throwIfNotEqual("isDateTime", left.isDateTime, right.isDateTime);
        left.isUuid = throwIfNotEqual("isUuid", left.isUuid, right.isUuid);
        left.isUri = throwIfNotEqual("isUri", left.isUri, right.isUri);
        left.isEmail = throwIfNotEqual("isEmail", left.isEmail, right.isEmail);
        left.isPassword = throwIfNotEqual("isPassword", left.isPassword, right.isPassword);
        left.isNull = throwIfNotEqual("isNull", left.isNull, right.isNull);
        left.isVoid = throwIfNotEqual("isVoid", left.isVoid, right.isVoid);
        left.isFreeFormObject = throwIfNotEqual("isFreeFormObject", left.isFreeFormObject, right.isFreeFormObject);
        left.isAnyType = left.isAnyType && right.isAnyType;
        left.isArray = throwIfNotEqual("isArray", left.isArray, right.isArray);
        left.isMap = throwIfNotEqual("isMap", left.isMap, right.isMap);
        left.isEnum = left.isEnum || right.isEnum;
        left.isInnerEnum = throwIfNotEqual("isInnerEnum", left.isInnerEnum, right.isInnerEnum);
        left.isEnumRef = left.isEnumRef || right.isEnumRef;
        left.isReadOnly = throwIfNotEqual("isReadOnly", left.isReadOnly, right.isReadOnly);
        left.isWriteOnly = throwIfNotEqual("isWriteOnly", left.isWriteOnly, right.isWriteOnly);
        left.isNullable = left.isNullable && right.isNullable;
        left.isSelfReference = throwIfNotEqual("isSelfReference", left.isSelfReference, right.isSelfReference);
        left.isCircularReference = throwIfNotEqual("isCircularReference", left.isCircularReference, right.isCircularReference);
        left.isDiscriminator = throwIfNotEqual("isDiscriminator", left.isDiscriminator, right.isDiscriminator);
        left.isNew = throwIfNotEqual("isNew", left.isNew, right.isNew);
        left.isOverridden = throwIfNotEqual("isOverridden", left.isOverridden, right.isOverridden);
        left._enum = throwIfNotEqual("_enum", left._enum, right._enum);
        left.allowableValues = mergeAllowableValues("allowableValues", left.allowableValues, right.allowableValues);
        left.items = throwIfNotEqual("items", left.items, right.items);
        left.additionalProperties = throwIfNotEqual("additionalProperties", left.additionalProperties, right.additionalProperties);
        left.vars = throwIfNotEqual("vars", left.vars, right.vars);
        left.requiredVars = throwIfNotEqual("requiredVars", left.requiredVars, right.requiredVars);
        left.mostInnerItems = throwIfNotEqual("mostInnerItems", left.mostInnerItems, right.mostInnerItems);
        left.vendorExtensions = throwIfNotEqual("vendorExtensions", left.vendorExtensions, right.vendorExtensions);
        left.hasValidation = throwIfNotEqual("hasValidation", left.hasValidation, right.hasValidation);
        left.isInherited = throwIfNotEqual("isInherited", left.isInherited, right.isInherited);
        left.discriminatorValue = throwIfNotEqual("discriminatorValue", left.discriminatorValue, right.discriminatorValue);
        left.nameInLowerCase = throwIfNotEqual("nameInLowerCase", left.nameInLowerCase, right.nameInLowerCase);
        left.nameInCamelCase = takeLeftIfNotNull("nameInCamelCase", left.nameInCamelCase, right.nameInCamelCase);
        left.nameInSnakeCase = takeLeftIfNotNull("nameInSnakeCase", left.nameInSnakeCase, right.nameInSnakeCase);
        left.enumName = throwIfNotEqual("enumName", left.enumName, right.enumName);
        left.maxItems = throwIfNotEqual("maxItems", left.maxItems, right.maxItems);
        left.minItems = throwIfNotEqual("minItems", left.minItems, right.minItems);
        left.setMaxProperties(throwIfNotEqual("getMaxProperties", left.getMaxProperties(), right.getMaxProperties()));
        left.setMinProperties(throwIfNotEqual("getMinProperties", left.getMinProperties(), right.getMinProperties()));
        left.setUniqueItems(throwIfNotEqual("getUniqueItems", left.getUniqueItems(), right.getUniqueItems()));
        left.isXmlAttribute = throwIfNotEqual("isXmlAttribute", left.isXmlAttribute, right.isXmlAttribute);
        left.xmlPrefix = throwIfNotEqual("xmlPrefix", left.xmlPrefix, right.xmlPrefix);
        left.xmlName = throwIfNotEqual("xmlName", left.xmlName, right.xmlName);
        left.xmlNamespace = throwIfNotEqual("xmlNamespace", left.xmlNamespace, right.xmlNamespace);
        left.isXmlWrapped = throwIfNotEqual("isXmlWrapped", left.isXmlWrapped, right.isXmlWrapped);
        left.setAdditionalPropertiesIsAnyType(throwIfNotEqual("getAdditionalPropertiesIsAnyType", left.getAdditionalPropertiesIsAnyType(), right.getAdditionalPropertiesIsAnyType()));
        left.setHasVars(throwIfNotEqual("getHasVars", left.getHasVars(), right.getHasVars()));
        left.setHasRequired(throwIfNotEqual("getHasRequired", left.getHasRequired(), right.getHasRequired()));
        left.setHasDiscriminatorWithNonEmptyMapping(throwIfNotEqual("getHasDiscriminatorWithNonEmptyMapping", left.getHasDiscriminatorWithNonEmptyMapping(), right.getHasDiscriminatorWithNonEmptyMapping()));
        left.setComposedSchemas(left.getComposedSchemas());
        left.setHasMultipleTypes(throwIfNotEqual("getHasMultipleTypes", left.getHasMultipleTypes(), right.getHasMultipleTypes()));
        left.setRequiredVarsMap(throwIfNotEqual("getRequiredVarsMap", left.getRequiredVarsMap(), right.getRequiredVarsMap()));
        left.setRef(takeLeftIfNotNull("getRef", left.getRef(), right.getRef()));
        left.setSchemaIsFromAdditionalProperties(throwIfNotEqual("getSchemaIsFromAdditionalProperties", left.getSchemaIsFromAdditionalProperties(), right.getSchemaIsFromAdditionalProperties()));
        left.setIsBooleanSchemaTrue(left.getIsBooleanSchemaTrue() && right.getIsBooleanSchemaTrue());
        left.setIsBooleanSchemaFalse(throwIfNotEqual("getIsBooleanSchemaFalse", left.getIsBooleanSchemaFalse(), right.getIsBooleanSchemaFalse()));
        left.setFormat(throwIfNotEqual("getFormat", left.getFormat(), right.getFormat()));
        left.setDependentRequired(throwIfNotEqual("getDependentRequired", left.getDependentRequired(), right.getDependentRequired()));
        left.setContains(throwIfNotEqual("getContains", left.getContains(), right.getContains()));
    }

    private static <T> T throwIfNotEqual(final String propertyName, final T left, final T right) {
        if (left == null && right == null || left != null && left.equals(right)) {
            return left;
        }
        throw new UnsupportedOperationException("Property " + propertyName + " is different for left and right.");
    }

    private static <T> T takeLeftIfNotNull(final String propertyName, final T left, final T right) {
        return left == null ? right : left;
    }

    private static String mergeTypes(final String propertyName, final String left, final String right) {
        if ("oas_any_type_not_mapped".equals(left)) {
            return right;
        }
        if ("oas_any_type_not_mapped".equals(right)) {
            return left;
        }
        if ("AnyType".equals(left)) {
            return right;
        }
        if ("AnyType".equals(right)) {
            return left;
        }
        if (left == null) {
            return right;
        }
        if (right == null) {
            return left;
        }
        if (left.equals(right)) {
            return left;
        }
        throw new UnsupportedOperationException("Property " + propertyName + " is different for left and right.");
    }

    private static Map<String, Object> mergeAllowableValues(
        final String propertyName,
        final Map<String, Object> left,
        final Map<String, Object> right) {
        if (left == null || left.isEmpty()) {
            return right;
        }
        if (right == null || right.isEmpty()) {
            return left;
        }
        if (left.containsKey("values") && right.containsKey("values") && left.size() == 1 && right.size() == 1) {
            final ArrayList<String> leftValues = (ArrayList<String>)left.get("values");
            final ArrayList<String> rightValues = (ArrayList<String>)right.get("values");
            final ArrayList<String> values = new ArrayList<>(leftValues);
            values.retainAll(rightValues);
            final Map<String, Object> result = new HashMap<>();
            result.put("values", values);
            return result;
        }
        throw new UnsupportedOperationException("Property " + propertyName + " is different for left and right.");
    }
}
