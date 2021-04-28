using System.Threading;
using System.Threading.Tasks;

namespace MyCustomHttpServer
{
    public interface IHttpServer
    {
        Task StartAsync(CancellationToken cancellationToken = default);

        Task StopAsync(CancellationToken cancellationToken = default);
    }
}