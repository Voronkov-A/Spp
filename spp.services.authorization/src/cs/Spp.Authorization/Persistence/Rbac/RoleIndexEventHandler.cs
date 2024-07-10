using Dapper;
using Spp.Authorization.Events.Rbac;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Miscellaneous;
using Spp.Authorization.Persistence.Schemas.Indices;
using Spp.Common.Mediator;
using Spp.Common.Cqs;

namespace Spp.Authorization.Persistence.Rbac;

public class RoleIndexEventHandler(IndicesDatabase database) :
    IRequestHandler<UpdateIndexCommand<RoleCreated>, Unit>,
    IRequestHandler<UpdateIndexCommand<RoleDeleted>, Unit>
{
    public async Task<Unit> Handle(UpdateIndexCommand<RoleCreated> request, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(
            "insert into indices.RoleIsDefaults (RoleId, IsDefault) values (@RoleId, @IsDefault);",
            new
            {
                RoleId = request.AggregateId.ToString(),
                IsDefault = request.Event.IsDefault
            },
            cancellationToken: cancellationToken);
        await using var connection = database.CreateConnection();
        await connection.ExecuteAsync(command);
        return default;
    }

    public async Task<Unit> Handle(UpdateIndexCommand<RoleDeleted> request, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(
            "delete from indices.RoleIsDefaults where RoleId = @RoleId;",
            new
            {
                RoleId = request.AggregateId.ToString(),
            },
            cancellationToken: cancellationToken);
        await using var connection = database.CreateConnection();
        await connection.ExecuteAsync(command);
        return default;
    }
}
