using System;
using System.Net;
using System.Net.Sockets;

namespace ARMDServer
{
    public static class Program
    {
        private static void TimeServer(UdpClient udp)
        {
            ReadOnlySpan<byte> requestData;
            var sender = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                requestData = udp.Receive(ref sender);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            var request = Request.FromSpan(requestData);

            Console.WriteLine(String.Format("{0:X4} - This is hex code.", request.Identifier));

            if (!request.IsValid)
            {
                return;
            }

            var response = new Response(request.CncTime);
            var responseData = response.ToArray();

            udp.Send(responseData, responseData.Length, sender);
        }

        static void Main(string[] args)
        {
            var serverEndPoint = new IPEndPoint(IPAddress.Any, 53847);
            var udp = new UdpClient(serverEndPoint);
            while (true)
            {
                TimeServer(udp);
            }
        }
    }
}
