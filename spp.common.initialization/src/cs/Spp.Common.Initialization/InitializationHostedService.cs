using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Initialization;

internal class InitializationHostedService(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        int index = 0;

        while (true)
        {
            await using var scope = serviceProvider.CreateAsyncScope();
            var initializer = scope.ServiceProvider.GetServices<IInitializer>().Skip(index++).FirstOrDefault();

            if (initializer == null)
            {
                break;
            }

            await initializer.Initialize(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
