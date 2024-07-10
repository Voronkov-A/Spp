namespace Spp.Authorization.Application.Rbac.Errors;

public readonly struct InvalidNameError(string name)
{
    public string Name { get; } = name;
}
