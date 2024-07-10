using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Spp.Common.Miscellaneous;
using Spp.IdentityProvider.Application.Users.Settings;
using Spp.IdentityProvider.Domain.Users;
using System;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Mediator;

namespace Spp.IdentityProvider.Application.Users.Commands;

public class CreateOrUpdateDefaultUsersCommandHandler(
    UserManager<User> userManager,
    IOptions<DefaultUserSetSettings> settings) :
    IRequestHandler<CreateOrUpdateDefaultUsersCommand, Unit>
{
    public async Task<Unit> Handle(CreateOrUpdateDefaultUsersCommand request, CancellationToken cancellationToken)
    {
        foreach (var userSettings in settings.Value.Users)
        {
            var user = await userManager.FindByNameAsync(userSettings.Username);

            IdentityResult result;

            if (user == null)
            {
                var id = userSettings.DefaultId == null ? new UserId() : new UserId(userSettings.DefaultId);
                user = new User(id, userSettings.Username);
                result = await userManager.CreateAsync(user, userSettings.Password);
            }
            else
            {
                user.PasswordHash = null;
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, userSettings.Password);
                result = await userManager.UpdateAsync(user);
            }

            if (!result.Succeeded)
            {
                throw new ApplicationException($"Error while creating user '{userSettings.Username}': {result}.");
            }
        }

        return default;
    }
}
