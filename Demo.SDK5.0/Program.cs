using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.SDK5._0
{
    class Program
    {
        /// <summary>
        /// 5.0 Test error, works fine after adding key code
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Key codes
            SocketsHttpHandler handler = new SocketsHttpHandler
            {
                ConnectCallback = IPv4ConnectAsync
            };
            var client = new HttpClient(handler);
            var resp = client.GetAsync("https://sfapi-sbox.sf-express.com/std/service").Result;
            //var resp = client.GetAsync("https://demo.identityserver.io/.well-known/openid-configuration").Result;
            Console.WriteLine(resp.Content.ReadAsStringAsync().Result);
            Console.ReadKey();
        }
        static async ValueTask<Stream> IPv4ConnectAsync(SocketsHttpConnectionContext context, CancellationToken cancellationToken)
        {
            // By default, we create dual-mode sockets:
            // Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.NoDelay = true;

            try
            {
                await socket.ConnectAsync(context.DnsEndPoint, cancellationToken).ConfigureAwait(false);
                return new NetworkStream(socket, ownsSocket: true);
            }
            catch
            {
                socket.Dispose();
                throw;
            }
        }

    }
}
