package com.spp.openapigenerators;

import org.openapitools.codegen.utils.CamelizeOption;
import org.openapitools.codegen.utils.StringUtils;

public class CodegenEnumValue {
    public final String value;

    public final String nameInPascalCase;

    public CodegenEnumValue(final String value) {
        this.value = value;
        nameInPascalCase = StringUtils.camelize(value, CamelizeOption.UPPERCASE_FIRST_CHAR);
    }
}
