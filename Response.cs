using System;
using System.IO;

namespace ARMDServer
{
    public class Response
    {
        readonly BinaryDateTime _cncTime;
        readonly BinaryDateTime _startUpTime;
        readonly BinaryDateTime _localTime;
        readonly uint _ticksFromStartUp;

        public Response(BinaryDateTime cncTime)
        {
            _cncTime = cncTime;
            _startUpTime = BinaryDateTime.FromDateTime(GetStartUpTime());
            _localTime = BinaryDateTime.FromDateTime(DateTime.Now);
            _ticksFromStartUp = (uint)Environment.TickCount;
        }

        private static DateTime GetStartUpTime()
        {
            var uptime = TimeSpan.FromMilliseconds(Environment.TickCount64);
            return DateTime.Now.Subtract(uptime);
        }

        public byte[] ToArray()
        {
            byte[] responseData = new byte[500];
            using MemoryStream responseDataStream = new MemoryStream(responseData);
            using BinaryWriter responseDataWriter = new BinaryWriter(responseDataStream);
            WriteTo(responseDataWriter);
            AppendChecksumTo(responseDataWriter);

            return responseData;
        }

        private void AppendChecksumTo(BinaryWriter responseDataWriter)
        {
            for (int i = 0; i < 4; i++)
            {
                WriteTo(responseDataWriter);
            }
        }

        private void WriteTo(BinaryWriter writer)
        {
            writer.Write(_cncTime);
            writer.Write(_startUpTime);
            writer.Write(_localTime);
            writer.Write(_ticksFromStartUp);
        }
    }
}