using System;
using System.Runtime.InteropServices;

namespace ARMDServer
{
    public static class SpanExtensions
    {
        public static ReadOnlySpan<byte> AsSpan<T>(this T str) where T : struct
        {
            return MemoryMarshal.Cast<T, byte>(MemoryMarshal.CreateSpan(ref str, 1));
        }

        public static T AsStruct<T>(this Span<byte> span) where T : struct
        {
            if (span == null)
            {
                throw new ArgumentNullException(nameof(span));
            }

            if (span.Length != Marshal.SizeOf(typeof(T)))
            {
                throw new SpanLengthException($"The size of span not equal to the size of {typeof(T).Name} struct.");
            }

            return MemoryMarshal.AsRef<T>(span);
        }
    }
}