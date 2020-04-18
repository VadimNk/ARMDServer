using System;
using System.Net;

namespace ARMDServer
{
    public static class Program
    {
        private const int DateTimeServerPort = 53847;

        static void Main()
        {
            var server = new DateTimeServer(IPAddress.Any, DateTimeServerPort);

            Console.Write("ARMD server starting... ");
            server.Start();
            Console.WriteLine("Done!");

            Console.WriteLine("Press Enter to stop the server or '!' to restart the server...");

            while (true)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                if (line == "!")
                {
                    Console.Write("Server restarting... ");
                    server.Restart();
                    Console.WriteLine("Done!");
                }
            }

            Console.Write("Server stopping... ");
            server.Stop();
            Console.WriteLine("Done!");
        }
    }
}
