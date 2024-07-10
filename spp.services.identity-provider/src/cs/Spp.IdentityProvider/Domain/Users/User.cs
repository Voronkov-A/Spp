using Microsoft.AspNetCore.Identity;

namespace Spp.IdentityProvider.Domain.Users;

public sealed class User : IdentityUser
{
    public User(UserId id, string username)
    {
        base.Id = id.ToString();
        UserName = username;
    }

    public new UserId Id => new(base.Id);

    private User()
    {
    }
}
