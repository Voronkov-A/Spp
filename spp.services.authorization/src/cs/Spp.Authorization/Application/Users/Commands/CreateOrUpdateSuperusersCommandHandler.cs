using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Spp.Authorization.Application.Users.Settings;
using Spp.Authorization.Domain.Users;
using Spp.Authorization.Domain.Users.Repositories;
using Spp.Common.Domain;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;

namespace Spp.Authorization.Application.Users.Commands;

public class CreateOrUpdateSuperusersCommandHandler(
    IUserRepository userRepository,
    IUserNameGenerator nameGenerator,
    IOptions<SuperuserSetSettings> settings) :
    IRequestHandler<CreateOrUpdateSuperusersCommand, Unit>
{
    public async Task<Unit> Handle(CreateOrUpdateSuperusersCommand request, CancellationToken cancellationToken)
    {
        foreach (var userSettings in settings.Value.Items)
        {
            var identities = userSettings.Identities
                .Select(x => new UserIdentity(providerId: x.ProviderId, subjectId: x.SubjectId))
                .ToHashSet();

            User? user = null;

            foreach (var identity in identities)
            {
                user = await userRepository.Find(identity, cancellationToken);

                if (user != null)
                {
                    break;
                }
            }

            if (user == null)
            {
                await CreateUser(identities, cancellationToken);
            }
            else
            {
                UpdateUser(user, identities);
            }
        }

        return default;
    }

    private async Task CreateUser(IEnumerable<UserIdentity> identities, CancellationToken cancellationToken)
    {
        var user = new User(
            id: new EntityId(),
            name: await nameGenerator.GenerateName(cancellationToken),
            isSuperuser: true,
            identities: identities);
        await userRepository.Add(user, cancellationToken);
    }

    private static void UpdateUser(User user, IEnumerable<UserIdentity> identities)
    {
        var identitiesToRemove = user.Identities.Where(x => !identities.Contains(x)).ToList();
        var identitiesToAdd = identities.Where(x => !user.HasIdentity(x)).ToList();

        if (identitiesToAdd.Count > 0)
        {
            user.AddIdentities(identitiesToAdd);
        }

        if (identitiesToRemove.Count > 0)
        {
            foreach (var identity in identitiesToRemove)
            {
                user.RemoveIdentity(identity);
            }
        }
    }
}
