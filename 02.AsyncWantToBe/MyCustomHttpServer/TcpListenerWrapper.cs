using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MyCustomHttpServer
{
    public class TcpListenerWrapper : ITcpListener
    {
        private readonly TcpListener _tcpListener;

        public TcpListenerWrapper()
        {
            _tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);
        }

        public Task<TcpClient> AcceptTcpClientAsync()
            => _tcpListener.AcceptTcpClientAsync();

        //public Task<NetworkStream> GetStreamAsync()
        //    => Task.FromResult(_tcpListener.GetStream());

        public Task StartAsync()
        {
            _tcpListener.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            _tcpListener.Stop();

            return Task.CompletedTask;
        }
    }
}
