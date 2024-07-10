package com.spp.openapigenerators;

import org.junit.Test;
import org.openapitools.codegen.ClientOptInput;
import org.openapitools.codegen.DefaultGenerator;
import org.openapitools.codegen.config.CodegenConfigurator;

public class SmokeTest {
    @Test
    public void launchSimpleCsharpClientCodegen() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
                .setGeneratorName("simple-csharp-client")
                .setInputSpec("src/test/resources/sample.yaml")
                .setOutputDir("out/test/SmokeTest/launchSimpleCsharpClientCodegen")
                .addAdditionalProperty("apiPath", "Api/{{name}}.cs")
                .addAdditionalProperty("modelPath", "Models/{{name}}.cs")
                .addAdditionalProperty("apiNamespace", "Api")
                .addAdditionalProperty("modelNamespace", "Models")
                .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void launchSimpleCsharpServerCodegen() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
                .setGeneratorName("simple-csharp-server")
                .setInputSpec("src/test/resources/sample.yaml")
                .setOutputDir("out/test/SmokeTest/launchSimpleCsharpServerCodegen")
                .addAdditionalProperty("apiPath", "Api/{{name}}.cs")
                .addAdditionalProperty("modelPath", "Models/{{name}}.cs")
                .addAdditionalProperty("apiNamespace", "Api")
                .addAdditionalProperty("modelNamespace", "Models")
                .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void launchSimpleCsharpTestClientCodegen() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
                .setGeneratorName("simple-csharp-test-client")
                .setInputSpec("src/test/resources/sample.yaml")
                .setOutputDir("out/test/SmokeTest/launchSimpleCsharpTestClientCodegen")
                .addAdditionalProperty("apiPath", "Api/{{name}}.cs")
                .addAdditionalProperty("modelPath", "Models/{{name}}.cs")
                .addAdditionalProperty("apiNamespace", "Api")
                .addAdditionalProperty("modelNamespace", "Models")
                .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }
}
