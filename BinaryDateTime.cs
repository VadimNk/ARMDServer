using System;
using System.Runtime.InteropServices;

namespace ARMDServer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BinaryDateTime
    {
        private ushort _year;
        private ushort _month;
        private ushort _dayOfWeek;
        private ushort _day;
        private ushort _hour;
        private ushort _minute;
        private ushort _second;
        private ushort _milliseconds;

        public static BinaryDateTime FromSpan(ReadOnlySpan<byte> span)
        {
            return MemoryMarshal.AsRef<BinaryDateTime>(span);
        }

        public static BinaryDateTime FromDateTime(DateTime dateTime)
        {
            return new BinaryDateTime
            {
                _year = (ushort)dateTime.Year,
                _month = (ushort)dateTime.Month,
                _dayOfWeek = (ushort)dateTime.DayOfWeek,
                _day = (ushort)dateTime.Day,
                _hour = (ushort)dateTime.Hour,
                _minute = (ushort)dateTime.Minute,
                _second = (ushort)dateTime.Second,
                _milliseconds = (ushort)dateTime.Millisecond
            };
        }

        public ReadOnlySpan<byte> AsSpan()
        {
            return MemoryMarshal.Cast<BinaryDateTime, byte>(MemoryMarshal.CreateSpan(ref this, 1));
        }
    }
}