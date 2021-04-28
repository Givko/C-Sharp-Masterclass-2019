using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCustomHttpServer
{
    public class HttpServer : IHttpServer
    {
        private bool isWorking;
        private readonly ITcpListener _tcpListener;

        public HttpServer(ITcpListener tcpListener)
        {
            _tcpListener = tcpListener
                ?? throw new ArgumentNullException(nameof(tcpListener));
        }

        public async Task StartAsync(
            CancellationToken cancellationToken = default)
        {
            await _tcpListener.StartAsync();

            this.isWorking = true;

            Console.WriteLine("Started.");

            while (this.isWorking)
            {
                var client = await _tcpListener.AcceptTcpClientAsync();
                await using var stream = client.GetStream();
                var buffer = new byte[1024];
                var readLength = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                var requestText = Encoding.UTF8.GetString(buffer, 0, readLength);

                Console.WriteLine(new string('=', 60));
                Console.WriteLine(requestText);

                var responseText = await File.ReadAllTextAsync("index.html", cancellationToken);
                var responseBytes = Encoding.UTF8.GetBytes(
                    "HTTP/1.0 200 Not Found" + Environment.NewLine +
                    "Content-Length: " + responseText.Length + Environment.NewLine + Environment.NewLine +
                    responseText);
                await stream.WriteAsync(responseBytes, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            this.isWorking = false;
            _tcpListener.StopAsync();

            return Task.CompletedTask;
        }
    }
}