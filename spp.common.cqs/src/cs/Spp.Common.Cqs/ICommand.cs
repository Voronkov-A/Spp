using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;

namespace Spp.Common.Cqs;

public interface ICommand<out TResponse> : IRequest<TResponse>;

public interface ICommand : ICommand<Unit>;
