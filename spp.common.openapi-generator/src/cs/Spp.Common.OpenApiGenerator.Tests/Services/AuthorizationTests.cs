using System.Threading;
using System.Threading.Tasks;
using Spp.Common.OpenApiGenerator.FileSystem;
using Spp.Common.OpenApiGenerator.Modules.SimpleCsharp.Constants;
using Spp.Common.OpenApiGenerator.Modules.SimpleCsharp.Server;
using Xunit;

namespace Spp.Common.OpenApiGenerator.Tests.Services;

public class AuthorizationTests
{
    [Fact]
    public async Task HostServiceV1()
    {
        var generator = new Generator<SimpleCsharpConstantsModuleParameters>(
            module: new SimpleCsharpConstantsModule(),
            documentResolver: new FileDocumentResolver(
                "../../../../../../../services/authorization/contracts/webapi.v1"),
            outputWriter: new FileGenerationOutputWriter(
                "../../../../../../../services/authorization/src/Spp.Authorization"));
        await generator.Generate(
            openApiDocumentPath: "authorization.service.yaml",
            parameters: new SimpleCsharpConstantsModuleParameters
            {
                OutputPath = "WebApi/Service/V1/Generated/Service.Generated.cs",
                Namespace = "Spp.Authorization.WebApi.Service.V1",
                PathsModelNameTemplate = "{{ documentPath | remove_first: \"authorization.\" | remove_last: \".yaml\" | pascal_case }}"
            },
            CancellationToken.None);
    }

    [Fact]
    public async Task HostAuthV1()
    {
        var generator = new Generator<SimpleCsharpServerModuleParameters>(
            module: new SimpleCsharpServerModule(),
            documentResolver: new FileDocumentResolver(
                "../../../../../../../services/authorization/contracts/webapi.v1"),
            outputWriter: new FileGenerationOutputWriter(
                "../../../../../../../services/authorization/src/Spp.Authorization"));
        await generator.Generate(
            openApiDocumentPath: "authorization.auth.yaml",
            parameters: new SimpleCsharpServerModuleParameters
            {
                OutputPath = "WebApi/Auth/V2/Generated/Service.Generated.cs",
                Namespace = "Spp.Authorization.WebApi.Auth.V1",
                PathsModelNameTemplate = "{{ documentPath | remove_first: \"authorization.\" | remove_last: \".yaml\" | pascal_case }}"
            },
            CancellationToken.None);
    }
}
