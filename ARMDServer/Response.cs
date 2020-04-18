using System;
using System.Runtime.InteropServices;

namespace ARMDServer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Response
    {
        public readonly BinaryDateTime CncTime { get; }
        public readonly BinaryDateTime StartUpTime { get; }
        public readonly BinaryDateTime LocalTime { get; }
        public readonly uint TicksFromStartUp { get; }

        public const int ChecksumRepeats = 4;

        public Response(BinaryDateTime cncTime)
        {
            CncTime = cncTime;
            StartUpTime = BinaryDateTime.FromDateTime(GetStartUpTime());
            LocalTime = BinaryDateTime.FromDateTime(DateTime.Now);
            TicksFromStartUp = (uint)Environment.TickCount;
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