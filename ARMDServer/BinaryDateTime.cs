using System;
using System.Runtime.InteropServices;

namespace ARMDServer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BinaryDateTime
    {
        public ushort Year { get; private set; }
        public ushort Month { get; private set; }
        public ushort DayOfWeek { get; private set; }
        public ushort Day { get; private set; }
        public ushort Hour { get; private set; }
        public ushort Minute { get; private set; }
        public ushort Second { get; private set; }
        public ushort Millisecond { get; private set; }

        public static BinaryDateTime FromSpan(ReadOnlySpan<byte> span)
        {
            return MemoryMarshal.AsRef<BinaryDateTime>(span);
        }

        public static BinaryDateTime FromDateTime(DateTime dateTime)
        {
            return new BinaryDateTime
            {
                Year = (ushort)dateTime.Year,
                Month = (ushort)dateTime.Month,
                DayOfWeek = (ushort)dateTime.DayOfWeek,
                Day = (ushort)dateTime.Day,
                Hour = (ushort)dateTime.Hour,
                Minute = (ushort)dateTime.Minute,
                Second = (ushort)dateTime.Second,
                Millisecond = (ushort)dateTime.Millisecond
            };
        }

        public ReadOnlySpan<byte> AsSpan()
        {
            return MemoryMarshal.Cast<BinaryDateTime, byte>(MemoryMarshal.CreateSpan(ref this, 1));
        }

        public override bool Equals(object obj)
        {
            return obj is BinaryDateTime time &&
                   Year == time.Year &&
                   Month == time.Month &&
                   DayOfWeek == time.DayOfWeek &&
                   Day == time.Day &&
                   Hour == time.Hour &&
                   Minute == time.Minute &&
                   Second == time.Second &&
                   Millisecond == time.Millisecond;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Year, Month, DayOfWeek, Day, Hour, Minute, Second, Millisecond);
        }
    }
}