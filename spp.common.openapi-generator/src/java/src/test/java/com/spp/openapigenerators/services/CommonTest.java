package com.spp.openapigenerators.services;

import org.junit.Test;
import org.openapitools.codegen.ClientOptInput;
import org.openapitools.codegen.DefaultGenerator;
import org.openapitools.codegen.config.CodegenConfigurator;

public class CommonTest {
    @Test
    public void hostAuthV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-server")
            .setInputSpec("../../../services/common/contracts/common.errors.v1.yaml")
            .setOutputDir("../../../modules/cs/Spp.Common.Services/Spp.Common.Services")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("modelPath", "Errors/V1/Generated/{{name}}.cs")
            .addAdditionalProperty("modelNamespace", "Spp.Common.Services.Errors.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }
}
