using System.Collections.Generic;
using System.Linq;
using Spp.Authorization.Domain.Common.Exceptions;

namespace Spp.Authorization.Domain.Rbac;

public readonly struct RoleName
{
    public RoleName(string def, IEnumerable<Translation> translations)
    {
        if (def.Length > 256)
        {
            throw new InvalidNameException("Value is too long.", def);
        }

        Default = def;
        Translations = translations.ToList();

        if (Translations.Count > 1024)
        {
            throw new InvalidNameException("Too many translations.", def);
        }
    }

    public string Default { get; }

    public IReadOnlyCollection<Translation> Translations { get; }
}
