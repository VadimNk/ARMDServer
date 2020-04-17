using System.IO;

namespace ARMDServer
{
    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter writer, BinaryDateTime dateTime)
        {
            dateTime.WriteTo(writer);
        }
    }
}