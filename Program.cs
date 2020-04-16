using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace bafro
{
   
    

    class Program
    {
        private static DateTime GetWindowsStartUpDateTime()
        {
            TimeSpan t = TimeSpan.FromMilliseconds(System.Environment.TickCount64);
            DateTime time = DateTime.Now.Subtract(t);
            return time;
        }
        private static void TimeServer(UdpClient newsock, IPEndPoint sender)

        {
            byte[] data = new byte[1024];
            //IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 53847);
            //UdpClient newsock = new UdpClient(ipep);
            //IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                data = newsock.Receive(ref sender);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
            //data.GetValue(short)
            using MemoryStream stream = new MemoryStream(data);
            using BinaryReader br = new BinaryReader(stream);
            Request request = new Request();
            request.identifier = br.ReadUInt32();
            Console.WriteLine(String.Format("{0:X4} - This is hex code.", request.identifier));
            request.pd = br.ReadInt16();
            request.type = br.ReadInt16();
            if (request.identifier != 0x42535253)
            {
                return;
            }

            if (request.pd != 1 || request.type != 1)
            {
                return;
            }

            SystemTime CNCtime = new SystemTime(br);
            //CNCtime.set(br);
            //DateTime dt = new DateTime();
            DateTime LocalTime = DateTime.Now.ToLocalTime();
            SystemTime st2 = new SystemTime(LocalTime);
            //st2.set(LocalTime);
            SystemTime startUp = new SystemTime(GetWindowsStartUpDateTime());
            //startUp.set(GetWindowsStartUpDateTime());
            Pack pack = new Pack(CNCtime, startUp, st2, (uint)System.Environment.TickCount);
            //pack.set(CNCtime, startUp, st2, (uint)System.Environment.TickCount);
            byte[] data2 = new byte[500];
            using MemoryStream stream2 = new MemoryStream(data2);
            using BinaryWriter br2 = new BinaryWriter(stream2);

            for(int i = 0; i < 5; i++)
            {
                pack.toBytes(br2);
            }
            newsock.Send(data2, data2.Length, sender);
        }
        static void Main(string[] args)
        {
            /*
            var array = new byte[14];

            array[0] = 5;
            array[3] = 10;

            array[5] = 222;
            array[4] = 111;
            
            var c = MemoryMarshal.AsRef<Test>(array.AsSpan());
*/
          //  return;
            //Console.WriteLine("Hello World!");
            //Console.WriteLine(GetWindowsStartUpDateTime());
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 53847);
            UdpClient newsock = new UdpClient(ipep);
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                TimeServer(newsock, sender);
            }

            Console.WriteLine("Fatal error, application will be closed.");

        }
    }
    struct Test
    {
        public byte a;
        public byte b;
        public byte c;
        public byte d;

        public ushort e;
        public ushort f;
    }
}
