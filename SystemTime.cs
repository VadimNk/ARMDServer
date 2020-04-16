using System;
using System.IO;

namespace bafro
{
class SystemTime
    {
        ushort wYear { get; set; }
        ushort wMonth { get; set; }
        ushort wDayOfWeek { get; set; }
        ushort wDay { get; set; }
        ushort wHour { get; set; }
        ushort wMinute { get; set; }
        ushort wSecond { get; set; }
        ushort wMilliseconds { get; set; }
        public SystemTime()
        {

        }
        public SystemTime(BinaryReader br)
        {
            set(br);            
        }
        public SystemTime(byte[] bytes)
        {
            using MemoryStream stream = new MemoryStream(bytes);
            using BinaryReader br = new BinaryReader(stream);
            set(br);
        }
        public SystemTime(DateTime dateTime)
        {
            set(dateTime);
        }
        public void set(BinaryReader br)
        {
            wYear = br.ReadUInt16();
            wMonth = br.ReadUInt16();
            wDayOfWeek = br.ReadUInt16();
            wDay = br.ReadUInt16();
            wHour = br.ReadUInt16();
            wMinute = br.ReadUInt16();
            wSecond = br.ReadUInt16();
            wMilliseconds = br.ReadUInt16();
        }
        public void set(DateTime dt)
        {
            wYear = (ushort)dt.Year;
            wMonth = (ushort)dt.Month;
            wDayOfWeek = (ushort)dt.DayOfWeek;
            wDay = (ushort)dt.Day;
            wHour = (ushort)dt.Hour;
            wMinute = (ushort)dt.Minute;
            wSecond = (ushort)dt.Second;
            wMilliseconds = (ushort)dt.Millisecond;
        }
        public BinaryWriter toBytes(BinaryWriter br)
        {
            br.Write(wYear);
            br.Write(wMonth);
            br.Write(wDayOfWeek);
            br.Write(wDay);
            br.Write(wHour);
            br.Write(wMinute);
            br.Write(wSecond);
            br.Write(wMilliseconds);
            return br;
        }
        public byte[] toBytes()
        {
            byte[] bytes = new byte[16];
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (BinaryWriter br = new BinaryWriter(stream))
                {
                    toBytes(br);
                }

            }
            return bytes;
        }
    }
}