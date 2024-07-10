package com.spp.openapigenerators;

import com.samskivert.mustache.Mustache;
import com.samskivert.mustache.Template;
import io.swagger.v3.oas.models.Operation;
import io.swagger.v3.oas.models.media.Schema;
import io.swagger.v3.oas.models.servers.Server;
import org.openapitools.codegen.*;
import org.openapitools.codegen.utils.CamelizeOption;
import org.openapitools.codegen.utils.StringUtils;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class SimpleCsharpClientCodegen extends DefaultCodegen implements CodegenConfig {

    protected String apiPath = "Api/{{name}}.cs";;
    protected String modelPath = "Models/{{name}}.cs";
    protected String apiNamespace = "Api";
    protected String modelNamespace = "Models";

    public CodegenType getTag() {
        return CodegenType.OTHER;
    }

    public String getName() {
        return "simple-csharp-client";
    }

    public String getHelp() {
        return "TODO: help.";
    }

    public SimpleCsharpClientCodegen() {
        super();

        apiNameSuffix = "";

        modelTemplateFiles.put("model.mustache", ".cs");
        apiTemplateFiles.put("api.mustache", ".cs");

        templateDir = "simple-csharp-client";

        apiPackage = "Api";
        modelPackage = "Models";

        addOption("apiPath", "", apiPath);
        addOption("modelPath", "", modelPath);
        addOption("apiNamespace", "", apiNamespace);
        addOption("modelNamespace", "", modelNamespace);

        additionalProperties.put("apiNamespace", apiNamespace);
        additionalProperties.put("modelNamespace", modelNamespace);

        reservedWords.add("default");
    }

    @Override
    public String escapeReservedWord(String name) {
        return "@" + name;
    }

    @Override
    public String modelFilename(String templateName, String modelName) {
        final Template template = Mustache.compiler().compile(modelPath);
        final Map<String, String> data = new HashMap<>();
        data.put("name", modelName);
        return outputFolder + "/" + template.execute(data);
    }

    @Override
    public String apiFilename(String templateName, String tag) {
        final Template template = Mustache.compiler().compile(apiPath);
        final Map<String, String> data = new HashMap<>();
        data.put("name", tag);
        return outputFolder + "/" + template.execute(data);
    }

    public String escapeQuotationMark(String input) {
        return input.replace("\"", "\\\"");
    }

    @Override
    public void processOpts() {
        super.processOpts();

        apiPath = (String) additionalProperties.getOrDefault("apiPath", apiPath);
        modelPath = (String) additionalProperties.getOrDefault("modelPath", modelPath);
        apiNamespace = (String) additionalProperties.getOrDefault("apiNamespace", apiNamespace);
        modelNamespace = (String) additionalProperties.getOrDefault("modelNamespace", modelNamespace);
    }

    @Override
    public CodegenProperty fromProperty(
        String name,
        Schema p,
        boolean required,
        boolean schemaIsFromAdditionalProperties
    ) {
        final CodegenProperty property = super.fromProperty(name, p, required, schemaIsFromAdditionalProperties);

        AllOfTransformer.transform(property);

        if (property.isString) {
            property.dataType = "string";
        }

        if (property.isUuid) {
            property.dataType = "System.Guid";
        }

        if (property.isBoolean) {
            property.dataType = "bool";
        }

        if (property.isLong) {
            property.dataType = "long";
        }

        if (property.isUri) {
            property.dataType = "System.Uri";
        }

        if (property.isArray) {
            property.dataType = "System.Collections.Generic.IReadOnlyList<" + property.items.dataType + ">";
        }

        if (property.additionalProperties != null) {
            property.dataType = "System.Collections.Generic.IReadOnlyDictionary<string, " + property.additionalProperties.dataType + ">";
        }

        if (property.isEnumRef) {
            property.isEnum = true;
        }

        return property;
    }

    @Override
    public CodegenModel fromModel(String name, Schema schema) {
        final CodegenModel model = super.fromModel(name, schema);

        final Map<String, Object> allowableValues = model.getAllowableValues();

        if (allowableValues != null) {
            final Object enumValuesObj = allowableValues.getOrDefault("enumValues", null);

            if (enumValuesObj == null) {
                final ArrayList<CodegenEnumValue> enumValues = new ArrayList<>();
                final ArrayList<String> values = (ArrayList<String>)allowableValues.get("values");

                for (final String value : values) {
                    enumValues.add(new CodegenEnumValue((value)));
                }

                allowableValues.put("enumValues", enumValues);
            }
        }

        return model;
    }

    @Override
    public CodegenOperation fromOperation(String path, String httpMethod, Operation operation, List<Server> servers) {
        final CodegenOperation result = super.fromOperation(path, httpMethod, operation, servers);

        for (final CodegenResponse response : result.responses) {
            if (response.code.equals("0")) {
                response.code = "Default";
            }

            for (final CodegenParameter header : response.getResponseHeaders()) {
                header.paramName = StringUtils.camelize(header.paramName, CamelizeOption.UPPERCASE_FIRST_CHAR);
            }
        }

        return result;
    }

    @Override
    public String toVarName(String name) {
        return toParamName(name);
    }
}
