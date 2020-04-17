using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ARMDServer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BinaryDateTime
    {
        ushort _year;
        ushort _month;
        ushort _dayOfWeek;
        ushort _day;
        ushort _hour;
        ushort _minute;
        ushort _second;
        ushort _milliseconds;

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