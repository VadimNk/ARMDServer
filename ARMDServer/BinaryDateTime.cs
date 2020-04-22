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
    }
}