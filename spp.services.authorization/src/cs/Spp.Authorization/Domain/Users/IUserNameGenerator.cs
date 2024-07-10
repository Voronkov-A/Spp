using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Domain.Users;

public interface IUserNameGenerator
{
    Task<UserName> GenerateName(CancellationToken cancellationToken);
}
