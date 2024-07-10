package com.spp.openapigenerators.services;

import org.junit.Test;
import org.openapitools.codegen.ClientOptInput;
import org.openapitools.codegen.DefaultGenerator;
import org.openapitools.codegen.config.CodegenConfigurator;

public class IdentityProviderTest {
    @Test
    public void hostServiceV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
                .setGeneratorName("simple-csharp-constants")
                .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.service.yaml")
                .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider")
                .addGlobalProperty("apis", "")
                .addAdditionalProperty("apiPath", "WebApi/Service/V1/Generated/{{name}}.Generated.cs")
                .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.WebApi.Service.V1");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void hostErrorsV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-server")
            .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.errors.yaml")
            .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("modelPath", "WebApi/Errors/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.WebApi.Errors.V1");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void hostAuthV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-server")
            .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.auth.yaml")
            .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "WebApi/Auth/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.WebApi.Auth.V1")
            .addAdditionalProperty("modelPath", "WebApi/Auth/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.WebApi.Auth.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void hostUsersV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
                .setGeneratorName("simple-csharp-server")
                .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.users.yaml")
                .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider")
                .addGlobalProperty("apis", "")
                .addGlobalProperty("models", "")
                .addAdditionalProperty("apiPath", "WebApi/Users/V1/Generated/{{name}}.Generated.cs")
                .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.WebApi.Users.V1")
                .addAdditionalProperty("modelPath", "WebApi/Users/V1/Generated/{{name}}.Generated.cs")
                .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.WebApi.Users.V1")
                .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
                .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void hostApplicationsV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-server")
            .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.applications.yaml")
            .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "WebApi/Applications/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.WebApi.Applications.V1")
            .addAdditionalProperty("modelPath", "WebApi/Applications/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.WebApi.Applications.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void clientServiceV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-client")
            .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.service.yaml")
            .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider.Client")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Service/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.Client.Service.V1")
            .addAdditionalProperty("modelPath", "Service/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.Client.Service.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void clientAuthV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-client")
            .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.auth.yaml")
            .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider.Client")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Auth/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.Client.Auth.V1")
            .addAdditionalProperty("modelPath", "Auth/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.Client.Auth.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void clientUsersV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-client")
            .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.users.yaml")
            .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider.Client")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Users/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.Client.Users.V1")
            .addAdditionalProperty("modelPath", "Users/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.Client.Users.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void clientConnectV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-client")
            .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.connect.yaml")
            .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider.Client")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Connect/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.Client.Connect.V1")
            .addAdditionalProperty("modelPath", "Connect/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.Client.Connect.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void clientApplicationsV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-client")
            .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.applications.yaml")
            .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider.Client")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Applications/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.Client.Applications.V1")
            .addAdditionalProperty("modelPath", "Applications/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.Client.Applications.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void testClientServiceV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
                .setGeneratorName("simple-csharp-test-client")
                .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.service.yaml")
                .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider.TestClient")
                .addGlobalProperty("apis", "")
                .addGlobalProperty("models", "")
                .addAdditionalProperty("apiPath", "Service/V1/Generated/{{name}}.Generated.cs")
                .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.TestClient.Service.V1")
                .addAdditionalProperty("modelPath", "Service/V1/Generated/{{name}}.Generated.cs")
                .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.TestClient.Service.V1")
                .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
                .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void testClientAuthV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-test-client")
            .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.auth.yaml")
            .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider.TestClient")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Auth/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.TestClient.Auth.V1")
            .addAdditionalProperty("modelPath", "Auth/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.TestClient.Auth.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void testClientUsersV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-test-client")
            .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.users.yaml")
            .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider.TestClient")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Users/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.TestClient.Users.V1")
            .addAdditionalProperty("modelPath", "Users/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.TestClient.Users.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void testClientConnectV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-test-client")
            .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.connect.yaml")
            .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider.TestClient")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Connect/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.TestClient.Connect.V1")
            .addAdditionalProperty("modelPath", "Connect/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.TestClient.Connect.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void testClientApplicationsV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-test-client")
            .setInputSpec("../../../spp.services.identity-provider/contracts/webapi.v1/identity-provider.applications.yaml")
            .setOutputDir("../../../spp.services.identity-provider/src/cs/Spp.IdentityProvider.TestClient")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Applications/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.IdentityProvider.TestClient.Applications.V1")
            .addAdditionalProperty("modelPath", "Applications/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.IdentityProvider.TestClient.Applications.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }
}
