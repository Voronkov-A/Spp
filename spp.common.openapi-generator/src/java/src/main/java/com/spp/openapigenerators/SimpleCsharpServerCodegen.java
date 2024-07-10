package com.spp.openapigenerators;

import com.samskivert.mustache.Mustache;
import com.samskivert.mustache.Template;
import io.swagger.v3.oas.models.Operation;
import io.swagger.v3.oas.models.media.Schema;
import io.swagger.v3.oas.models.servers.Server;
import org.openapitools.codegen.*;
import org.openapitools.codegen.model.*;
import org.openapitools.codegen.utils.CamelizeOption;
import org.openapitools.codegen.utils.StringUtils;

import java.util.*;

import static org.openapitools.codegen.utils.StringUtils.escape;

public class SimpleCsharpServerCodegen extends DefaultCodegen implements CodegenConfig {

  protected String apiPath = "Api/{{name}}.cs";;
  protected String modelPath = "Models/{{name}}.cs";
  protected String apiNamespace = "Api";
  protected String modelNamespace = "Models";

  public CodegenType getTag() {
    return CodegenType.OTHER;
  }

  public String getName() {
    return "simple-csharp-server";
  }

  /**
   * Provides an opportunity to inspect and modify operation data before the code is generated.
   */
  @Override
  public OperationsMap postProcessOperationsWithModels(OperationsMap objs, List<ModelMap> allModels) {

    // to try debugging your code generator:
    // set a break point on the next line.
    // then debug the JUnit test called LaunchGeneratorInDebugger

    OperationsMap results = super.postProcessOperationsWithModels(objs, allModels);

    OperationMap ops = results.getOperations();
    List<CodegenOperation> opList = ops.getOperation();

    // iterate over the operation and perhaps modify something
    for(CodegenOperation co : opList){
      // example:
      // co.httpMethod = co.httpMethod.toLowerCase();
    }

    return results;
  }

  public String getHelp() {
    return "TODO: help.";
  }

  public SimpleCsharpServerCodegen() {
    super();

    apiNameSuffix = "";

    // set the output folder here
    //outputFolder = "generated-code/my-codegen";

    modelTemplateFiles.put("model.mustache", ".cs");
    apiTemplateFiles.put("api.mustache", ".cs");

    templateDir = "simple-csharp-server";

    apiPackage = "Api";
    modelPackage = "Models";

    /*apiPath = "Api/{{name}}.cs";
    modelPath = "Models/{{name}}.cs";
    apiNamespace = "Api";
    modelNamespace = "Models";*/

    addOption("apiPath", "", apiPath);
    addOption("modelPath", "", modelPath);
    addOption("apiNamespace", "", apiNamespace);
    addOption("modelNamespace", "", modelNamespace);

    additionalProperties.put("apiNamespace", apiNamespace);
    additionalProperties.put("modelNamespace", modelNamespace);

    reservedWords.add("default");

    /**
     * Reserved words.  Override this with reserved words specific to your language
     */
    //reservedWords = new HashSet<String> (
    //  Arrays.asList(
    //    "sample1",  // replace with static values
    //    "sample2")
    //);

    /**
     * Additional Properties.  These values can be passed to the templates and
     * are available in models, apis, and supporting files
     */
    //additionalProperties.put("apiVersion", apiVersion);

    /**
     * Supporting Files.  You can write single files for the generator with the
     * entire object tree available.  If the input file has a suffix of `.mustache
     * it will be processed by the template engine.  Otherwise, it will be copied
     */
    //supportingFiles.add(new SupportingFile("myFile.mustache",   // the input template or file
    //  "",                                                       // the destination folder, relative `outputFolder`
    //  "myFile.sample")                                          // the output file
    //);

    /**
     * Language Specific Primitives.  These types will not trigger imports by
     * the client generator
     */
    //languageSpecificPrimitives = new HashSet<String>(
    //  Arrays.asList(
    //    "Type1",      // replace these with your types
    //    "Type2")
    //);
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

    /*if (model.getAdditionalPropertiesIsAnyType()) {
      model.parent = null;
      final CodegenProperty additionalPropertiesProperty = new CodegenProperty();
      additionalPropertiesProperty.setName("extensions");
      additionalPropertiesProperty.setNameInCamelCase("Extensions");
      additionalPropertiesProperty.setAdditionalPropertiesIsAnyType(true);
      additionalPropertiesProperty.setDataType("Dictionary<string, object?>");
      additionalPropertiesProperty.setRequired(true);
      model.vars.add(additionalPropertiesProperty);
      model.requiredVars.add(additionalPropertiesProperty);
    }*/

    return model;
  }

  @Override
  public CodegenOperation fromOperation(String path, String httpMethod, Operation operation, List<Server> servers) {
    final CodegenOperation result = super.fromOperation(path, httpMethod, operation, servers);

    result.httpMethod
        = "Microsoft.AspNetCore.Mvc.Http"
        + StringUtils.camelize(result.httpMethod.toLowerCase(), CamelizeOption.UPPERCASE_FIRST_CHAR);

    for (final CodegenResponse response : result.responses) {
      if (response.code.equals("0")) {
        response.code = "Default";
      }

        for (final CodegenParameter header : response.getResponseHeaders()) {
            header.paramName = StringUtils.camelize(header.paramName, CamelizeOption.UPPERCASE_FIRST_CHAR);
        }
    }

    /*if (!result.responses.isEmpty()) {
      final List<String> uniqueResponseTypes = new ArrayList<>();

      final StringBuilder returnTypeBuilder = new StringBuilder("Either");
      String separator = "<";

      for (final CodegenResponse response : result.responses) {
        if (!uniqueResponseTypes.contains(response.dataType)) {
          uniqueResponseTypes.add(response.dataType);
        }
      }

      for (final String responseType : uniqueResponseTypes) {
        returnTypeBuilder
                .append(separator)
                .append("IActionResult<")
                .append(responseType)
                .append('>');
        separator = ", ";
      }

      returnTypeBuilder.append('>');
      result.returnType = returnTypeBuilder.toString();
    }*/

    return result;
  }

    @Override
    public String toVarName(String name) {
      return toParamName(name);
    }
}
