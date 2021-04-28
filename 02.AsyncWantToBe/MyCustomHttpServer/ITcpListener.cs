using System.Net.Sockets;
using System.Threading.Tasks;

namespace MyCustomHttpServer
{
    public interface ITcpListener
    {
        Task StartAsync();
        Task StopAsync();
        Task<TcpClient> AcceptTcpClientAsync();
        //Task<NetworkStream> GetStreamAsync();
    }
}