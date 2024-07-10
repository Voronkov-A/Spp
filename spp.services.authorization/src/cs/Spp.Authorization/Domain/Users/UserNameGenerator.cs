using Spp.Authorization.Domain.GeneratedUserNameParts;
using Spp.Authorization.Domain.GeneratedUserNameParts.Repositories;
using Spp.Authorization.Domain.Users.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Domain.Users;

public class UserNameGenerator(
    IGeneratedUserNamePartRepository generatedUserNamePartRepository,
    IUserRepository userRepository) :
    IUserNameGenerator
{
    public async Task<UserName> GenerateName(CancellationToken cancellationToken)
    {
        UserName name;

        do
        {
            name = await Generate(cancellationToken);
        }
        while (await userRepository.Exists(name, cancellationToken));

        return name;
    }

    private async Task<UserName> Generate(CancellationToken cancellationToken)
    {
        var firstName = await GetNamePart(GeneratedUserNamePartType.FirstName, cancellationToken);
        var lastName = await GetNamePart(GeneratedUserNamePartType.LastName, cancellationToken);
        var stem = new GeneratedUserNameStem(firstName, lastName);
        var lastUser = await userRepository.FindLast(stem, cancellationToken);

        if (lastUser == null)
        {
            return UserName.CreateGenerated(stem.ToString());
        }
        else
        {
            var number = NextNumber(lastUser);
            return UserName.CreateGenerated($"{stem} {number}");
        }
    }

    private async Task<string> GetNamePart(GeneratedUserNamePartType type, CancellationToken cancellationToken)
    {
        return (await generatedUserNamePartRepository.FindRandom(type, cancellationToken))?.Value ?? "Anonymous";
    }

    private static long NextNumber(User user)
    {
        var name = user.Name.ToString();
        var lastChar = name[^1];

        if (!char.IsNumber(lastChar))
        {
            return 2;
        }

        var numberStartIndex = name.LastIndexOf(' ') + 1;
        var number = long.Parse(name[numberStartIndex..]);
        return number + 1;
    }
}
