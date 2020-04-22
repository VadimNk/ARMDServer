using System;
using System.Runtime.InteropServices;

namespace ARMDServer
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Request
    {
        public readonly uint Identifier { get; }
        public readonly ushort Pd { get; }
        public readonly ushort Type { get; }
        public readonly BinaryDateTime CncTime { get; }

        public bool IsValid => Identifier == 0x42535253 && Pd == 1 && Type == 1;
    }
}