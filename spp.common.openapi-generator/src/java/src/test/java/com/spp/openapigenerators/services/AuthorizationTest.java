package com.spp.openapigenerators.services;

import org.junit.Test;
import org.openapitools.codegen.ClientOptInput;
import org.openapitools.codegen.DefaultGenerator;
import org.openapitools.codegen.config.CodegenConfigurator;

public class AuthorizationTest {
    @Test
    public void hostServiceV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-constants")
            .setInputSpec("../../../spp.services.authorization/contracts/webapi.v1/authorization.service.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization")
            .addGlobalProperty("apis", "")
            .addAdditionalProperty("apiPath", "WebApi/Service/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.Authorization.WebApi.Service.V1");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void hostCommonV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
                .setGeneratorName("simple-csharp-server")
                .setInputSpec("../../../spp.services.authorization/contracts/webapi.v1/authorization.common.yaml")
                .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization")
                .addGlobalProperty("models", "")
                .addAdditionalProperty("modelPath", "WebApi/Common/V1/Generated/{{name}}.Generated.cs")
                .addAdditionalProperty("modelNamespace", "Spp.Authorization.WebApi.Common.V1");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void hostAuthV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-server")
            .setInputSpec("../../../spp.services.authorization/contracts/webapi.v1/authorization.auth.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "WebApi/Auth/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.Authorization.WebApi.Auth.V1")
            .addAdditionalProperty("modelPath", "WebApi/Auth/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.Authorization.WebApi.Auth.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void hostCallbackV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-constants")
            .setInputSpec("../../../spp.services.authorization/contracts/webapi.v1/authorization.callback.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization")
            .addGlobalProperty("apis", "")
            .addAdditionalProperty("apiPath", "WebApi/Callback/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.Authorization.WebApi.Callback.V1");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void hostRbacV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-server")
            .setInputSpec("../../../spp.services.authorization/contracts/webapi.v1/authorization.rbac.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "WebApi/Rbac/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.Authorization.WebApi.Rbac.V1")
            .addAdditionalProperty("modelPath", "WebApi/Rbac/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.Authorization.WebApi.Rbac.V1")
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
            .setInputSpec("../../../spp.services.authorization/contracts/webapi.v1/authorization.users.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "WebApi/Users/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.Authorization.WebApi.Users.V1")
            .addAdditionalProperty("modelPath", "WebApi/Users/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.Authorization.WebApi.Users.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void clientRbac() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
                .setGeneratorName("simple-csharp-client")
                .setInputSpec("../../../spp.services.authorization/contracts/webapi.v1/authorization.rbac.yaml")
                .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization.Client")
                .addGlobalProperty("apis", "")
                .addGlobalProperty("models", "")
                .addAdditionalProperty("apiPath", "Rbac/V1/Generated/{{name}}.Generated.cs")
                .addAdditionalProperty("apiNamespace", "Spp.Authorization.Client.Auth.V1")
                .addAdditionalProperty("modelPath", "Rbac/V1/Generated/{{name}}.Generated.cs")
                .addAdditionalProperty("modelNamespace", "Spp.Authorization.Client.Auth.V1")
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
            .setInputSpec("../../../spp.services.authorization/contracts/webapi.v1/authorization.service.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization.TestClient")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Service/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.Authorization.TestClient.Service.V1")
            .addAdditionalProperty("modelPath", "Service/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.Authorization.TestClient.Service.V1")
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
            .setInputSpec("../../../spp.services.authorization/contracts/webapi.v1/authorization.auth.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization.TestClient")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Auth/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.Authorization.TestClient.Auth.V1")
            .addAdditionalProperty("modelPath", "Auth/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.Authorization.TestClient.Auth.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void testClientCallbackV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-test-client")
            .setInputSpec("../../../spp.services.authorization/contracts/webapi.v1/authorization.callback.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization.TestClient")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Callback/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.Authorization.TestClient.Callback.V1")
            .addAdditionalProperty("modelPath", "Callback/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.Authorization.TestClient.Callback.V1")
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
            .setInputSpec("../../../spp.services.authorization/contracts/webapi.v1/authorization.connect.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization.TestClient")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Connect/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.Authorization.TestClient.Connect.V1")
            .addAdditionalProperty("modelPath", "Connect/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.Authorization.TestClient.Connect.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void testClientRbacV1() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-test-client")
            .setInputSpec("../../../spp.services.authorization/contracts/webapi.v1/authorization.rbac.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization.TestClient")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Rbac/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.Authorization.TestClient.Rbac.V1")
            .addAdditionalProperty("modelPath", "Rbac/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.Authorization.TestClient.Rbac.V1")
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
            .setInputSpec("../../../spp.services.authorization/contracts/webapi.v1/authorization.users.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization.TestClient")
            .addGlobalProperty("apis", "")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("apiPath", "Users/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("apiNamespace", "Spp.Authorization.TestClient.Users.V1")
            .addAdditionalProperty("modelPath", "Users/V1/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.Authorization.TestClient.Users.V1")
            .addTypeMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails")
            .addSchemaMapping("ProblemDetails", "Microsoft.AspNetCore.Mvc.ProblemDetails");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void eventsRbac() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-server")
            .setInputSpec("../../../spp.services.authorization/contracts/events/authorization.rbac.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization.Events")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("modelPath", "Rbac/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.Authorization.Events.Rbac");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void eventsGeneratedUserNameParts() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-server")
            .setInputSpec("../../../spp.services.authorization/contracts/events/authorization.generated-user-name-parts.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization.Events")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("modelPath", "GeneratedUserNameParts/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.Authorization.Events.GeneratedUserNameParts");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }

    @Test
    public void eventsUsers() {
        final CodegenConfigurator configurator = new CodegenConfigurator()
            .setGeneratorName("simple-csharp-server")
            .setInputSpec("../../../spp.services.authorization/contracts/events/authorization.users.yaml")
            .setOutputDir("../../../spp.services.authorization/src/cs/Spp.Authorization.Events")
            .addGlobalProperty("models", "")
            .addAdditionalProperty("modelPath", "Users/Generated/{{name}}.Generated.cs")
            .addAdditionalProperty("modelNamespace", "Spp.Authorization.Events.Users");

        final ClientOptInput clientOptInput = configurator.toClientOptInput();
        DefaultGenerator generator = new DefaultGenerator();
        generator.opts(clientOptInput).generate();
    }
}
