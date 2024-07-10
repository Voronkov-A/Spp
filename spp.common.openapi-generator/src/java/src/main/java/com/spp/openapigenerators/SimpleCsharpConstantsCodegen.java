package com.spp.openapigenerators;

import com.samskivert.mustache.Mustache;
import com.samskivert.mustache.Template;
import org.openapitools.codegen.*;

import java.util.HashMap;
import java.util.Map;

public class SimpleCsharpConstantsCodegen extends DefaultCodegen implements CodegenConfig {

    protected String apiPath = "Api/{{name}}.cs";;
    //protected String modelPath = "Models/{{name}}.cs";
    protected String apiNamespace = "Api";
    //protected String modelNamespace = "Models";

    public CodegenType getTag() {
        return CodegenType.OTHER;
    }

    public String getName() {
        return "simple-csharp-constants";
    }

    public String getHelp() {
        return "TODO: help.";
    }

    public SimpleCsharpConstantsCodegen() {
        super();

        apiNameSuffix = "";

        //modelTemplateFiles.put("model.mustache", ".cs");
        apiTemplateFiles.put("api.mustache", ".cs");

        templateDir = "simple-csharp-constants";

        apiPackage = "Api";
        modelPackage = "Models";

        addOption("apiPath", "", apiPath);
        //addOption("modelPath", "", modelPath);
        addOption("apiNamespace", "", apiNamespace);
        //addOption("modelNamespace", "", modelNamespace);

        additionalProperties.put("apiNamespace", apiNamespace);
        //additionalProperties.put("modelNamespace", modelNamespace);

        reservedWords.add("default");
    }

    @Override
    public String escapeReservedWord(String name) {
        return "@" + name;
    }

    /*@Override
    public String modelFilename(String templateName, String modelName) {
        final Template template = Mustache.compiler().compile(modelPath);
        final Map<String, String> data = new HashMap<>();
        data.put("name", modelName);
        return outputFolder + "/" + template.execute(data);
    }*/

    @Override
    public String apiFilename(String templateName, String tag) {
        final Template template = Mustache.compiler().compile(apiPath);
        final Map<String, String> data = new HashMap<>();
        data.put("name", tag);
        return outputFolder + "/" + template.execute(data);
    }

    @Override
    public String escapeQuotationMark(String input) {
        return input.replace("\"", "\\\"");
    }

    @Override
    public void processOpts() {
        super.processOpts();

        apiPath = (String) additionalProperties.getOrDefault("apiPath", apiPath);
        //modelPath = (String) additionalProperties.getOrDefault("modelPath", modelPath);
        apiNamespace = (String) additionalProperties.getOrDefault("apiNamespace", apiNamespace);
        //modelNamespace = (String) additionalProperties.getOrDefault("modelNamespace", modelNamespace);
    }
}
