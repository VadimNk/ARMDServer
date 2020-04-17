using System;
using System.Runtime.InteropServices;

namespace ARMDServer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Request
    {
        private uint _identifier;
        private long _pd;
        private long _type;
        private BinaryDateTime _cncTime;

        public uint Identifier
        {
            get { return _identifier; }
            private set
            {
                _identifier = value;
            }
        }

        public long Pd
        {
            get { return _pd; }
            private set
            {
                _pd = value;
            }
        }

        public long Type
        {
            get { return _type; }
            private set
            {
                _type = value;
            }
        }

        public BinaryDateTime CncTime
        {
            get { return _cncTime; }
            private set
            {
                _cncTime = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return Identifier == 0x42535253 && Pd == 1 && Type == 1;
            }
        }

        public static Request FromSpan(ReadOnlySpan<byte> span)
        {
            return MemoryMarshal.AsRef<Request>(span);
        }
    }
}