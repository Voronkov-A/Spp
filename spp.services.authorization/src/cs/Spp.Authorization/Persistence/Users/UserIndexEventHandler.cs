using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Spp.Authorization.Events.Users;
using Spp.Authorization.Persistence.Schemas.Indices;
using Spp.Common.Cqs;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;

namespace Spp.Authorization.Persistence.Users;

public class UserIndexEventHandler(IndicesDatabase database) :
    IRequestHandler<UpdateIndexCommand<UserCreated>, Unit>,
    IRequestHandler<UpdateIndexCommand<UserRenamed>, Unit>
{
    public async Task<Unit> Handle(UpdateIndexCommand<UserCreated> request, CancellationToken cancellationToken)
    {
        await using var connection = database.CreateConnection();
        var command = new CommandDefinition(
            """
            insert into indices.UserIdentities (UserId, ProviderId, SubjectId)
            select @UserId, unnest(@ProviderIds), unnest(@SubjectIds);

            insert into indices.UserNames (UserId, Value) values (@UserId, @Name);
            """,
            new
            {
                UserId = request.AggregateId.ToString(),
                ProviderIds = request.Event.Identities.Select(x => x.ProviderId).ToArray(),
                SubjectIds = request.Event.Identities.Select(x => x.SubjectId).ToArray(),
                Name = request.Event.Name
            },
            cancellationToken: cancellationToken);
        await connection.ExecuteAsync(command);
        return default;
    }

    public async Task<Unit> Handle(UpdateIndexCommand<UserRenamed> request, CancellationToken cancellationToken)
    {
        await using var connection = database.CreateConnection();
        var command = new CommandDefinition(
            "update indices.UserNames set Value = @Name where @UserId = @UserId;",
            new
            {
                UserId = request.AggregateId.ToString(),
                Name = request.Event.Name
            },
            cancellationToken: cancellationToken);
        await connection.ExecuteAsync(command);
        return default;
    }
}
