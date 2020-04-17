using System;
using System.Runtime.InteropServices;

namespace ARMDServer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Response
    {
        readonly BinaryDateTime _cncTime;
        readonly BinaryDateTime _startUpTime;
        readonly BinaryDateTime _localTime;
        readonly uint _ticksFromStartUp;

        private const int ChecksumRepeats = 4;

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
            var thisSize = Marshal.SizeOf(this);
            var checksumSize = thisSize * ChecksumRepeats;
            var responseSize = thisSize + checksumSize;

            var responseData = new byte[responseSize];

            AsSpan().CopyTo(responseData);
            AppendChecksumTo(responseData);

            return responseData;
        }

        private void AppendChecksumTo(Span<byte> destination)
        {
            var checksum = AsSpan();
            var size = Marshal.SizeOf(this);
            var offset = size;

            for (int i = 0; i < ChecksumRepeats; i++)
            {
                checksum.CopyTo(destination.Slice(offset + i * size));
            }
        }

        private ReadOnlySpan<byte> AsSpan()
        {
            return MemoryMarshal.Cast<Response, byte>(MemoryMarshal.CreateSpan(ref this, 1));
        }
    }
}