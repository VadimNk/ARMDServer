using System;
using System.IO;

namespace ARMDServer
{
    public class BinaryDateTime
    {
        ushort Year { get; set; }
        ushort Month { get; set; }
        ushort DayOfWeek { get; set; }
        ushort Day { get; set; }
        ushort Hour { get; set; }
        ushort Minute { get; set; }
        ushort Second { get; set; }
        ushort Milliseconds { get; set; }

        public static BinaryDateTime FromBinaryReader(BinaryReader reader)
        {
            return new BinaryDateTime
            {
                Year = reader.ReadUInt16(),
                Month = reader.ReadUInt16(),
                DayOfWeek = reader.ReadUInt16(),
                Day = reader.ReadUInt16(),
                Hour = reader.ReadUInt16(),
                Minute = reader.ReadUInt16(),
                Second = reader.ReadUInt16(),
                Milliseconds = reader.ReadUInt16()
            };
        }

        public static BinaryDateTime FromArray(byte[] array)
        {
            using MemoryStream stream = new MemoryStream(array);
            using BinaryReader reader = new BinaryReader(stream);
            return FromBinaryReader(reader);
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
                Milliseconds = (ushort)dateTime.Millisecond
            };
        }

        public void WriteTo(BinaryWriter writer)
        {
            writer.Write(Year);
            writer.Write(Month);
            writer.Write(DayOfWeek);
            writer.Write(Day);
            writer.Write(Hour);
            writer.Write(Minute);
            writer.Write(Second);
            writer.Write(Milliseconds);
        }

        public byte[] ToArray()
        {
            var bytes = new byte[16];
            using var stream = new MemoryStream(bytes);
            using var writer = new BinaryWriter(stream);
            WriteTo(writer);
            return bytes;
        }
    }
}