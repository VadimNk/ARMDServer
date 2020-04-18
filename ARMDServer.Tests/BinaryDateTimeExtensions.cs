using System;

namespace ARMDServer.Tests
{
    internal static class BinaryDateTimeExtensions
    {
        internal static DateTime ToDateTime(this BinaryDateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.Millisecond);
        }
    }
}