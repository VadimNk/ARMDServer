using System;
using System.Runtime.InteropServices;

namespace ARMDServer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Request
    {
        private readonly uint _identifier;
        private readonly long _pd;
        private readonly long _type;
        private readonly BinaryDateTime _cncTime;

        public BinaryDateTime CncTime => _cncTime;

        public bool IsValid => _identifier == 0x42535253 && _pd == 1 && _type == 1;

        public static Request FromSpan(ReadOnlySpan<byte> span)
        {
            return MemoryMarshal.AsRef<Request>(span);
        }
    }
}