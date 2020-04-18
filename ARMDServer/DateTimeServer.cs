using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using NetCoreServer;

namespace ARMDServer
{
    public class DateTimeServer : UdpServer
    {
        public DateTimeServer(IPAddress address, int port) : base(address, port) { }

        protected override void OnStarted()
        {
            ReceiveAsync();
        }

        protected override void OnReceived(EndPoint endpoint, byte[] buffer, long offset, long size)
        {
            Request request;
            try
            {
                request = Request.FromSpan(buffer);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            if (!request.IsValid)
            {
                return;
            }

            var responseData = new Response(request.CncTime).ToArray();
            SendAsync(endpoint, responseData, 0, responseData.Length);
        }

        protected override void OnSent(EndPoint endpoint, long sent)
        {
            ReceiveAsync();
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"DateTime server caught an error with code {error}");
        }
    }
}