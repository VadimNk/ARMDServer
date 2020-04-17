using System.IO;

namespace ARMDServer
{
    public class Request
    {
        public uint Identifier { get; set; }
        public long Pd { get; set; }
        public long Type { get; set; }
        public BinaryDateTime CncTime { get; set; }
        public bool IsValid => Identifier == 0x42535253 && Pd == 1 && Type == 1;

        public static Request FromArray(byte[] array)
        {
            using MemoryStream stream = new MemoryStream(array);
            using BinaryReader reader = new BinaryReader(stream);
            return Request.FromBinaryReader(reader);
        }

        public static Request FromBinaryReader(BinaryReader reader)
        {
            var request = new Request
            {
                Identifier = reader.ReadUInt32(),
                Pd = reader.ReadInt16(),
                Type = reader.ReadInt16()
            };

            if (request.IsValid)
            {
                request.CncTime = BinaryDateTime.FromBinaryReader(reader);
            }

            return request;
        }
    }
}