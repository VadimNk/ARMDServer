using System;
using System.Runtime.InteropServices;

namespace ARMDServer
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Request
    {
        public readonly uint Identifier { get; }
        public readonly long Pd { get; }
        public readonly long Type { get; }
        public readonly BinaryDateTime CncTime { get; }

        public bool IsValid => Identifier == 0x42535253 && Pd == 1 && Type == 1;

        public static Request FromSpan(ReadOnlySpan<byte> span)
        {
            if (span == null)
            {
                throw new ArgumentException(nameof(span));
            }

            if (span.Length != Marshal.SizeOf(typeof(Request)))
            {
                throw new RequestLengthException("The size of span not equal to the size of Request struct.");
            }

            return MemoryMarshal.AsRef<Request>(span);
        }
    }
}