using Spp.Common.Cqs;

namespace Spp.Authorization.Application.Auth.Commands;

public readonly struct RegisterInIdentityProviderCommand : ICommand, IRequiresUnitOfWork;
