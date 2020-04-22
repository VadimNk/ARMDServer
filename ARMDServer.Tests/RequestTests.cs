using System;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace ARMDServer.Tests
{
    public class RequestTests
    {
        private const uint ValidIdentifier = 0x42535253;
        private const ushort ValidPd = 1;
        private const ushort ValidType = 1;

        private BinaryDateTime TestCncTime { get; set; }

        [SetUp]
        public void Setup()
        {
            TestCncTime = BinaryDateTime.FromDateTime(DateTime.Now);
        }

        [Test]
        public void AbsoluteSizeOfType()
        {
            var request = new Request();
            var expectedSize =
                Marshal.SizeOf(request.Identifier) +
                Marshal.SizeOf(request.Pd) +
                Marshal.SizeOf(request.Type) +
                Marshal.SizeOf(request.CncTime);

            var actualSize = Marshal.SizeOf(request);

            Assert.AreEqual(expectedSize, actualSize);
        }

        [Test]
        public void ConvertionFromSpan()
        {
            var expected = new byte[Marshal.SizeOf(typeof(Request))];
            BitConverter.GetBytes(ValidIdentifier).CopyTo(expected, 0);
            BitConverter.GetBytes(ValidPd).CopyTo(expected, 4);
            BitConverter.GetBytes(ValidType).CopyTo(expected, 6);
            TestCncTime.AsSpan().ToArray().CopyTo(expected, 8);

            var request = expected.AsSpan().AsStruct<Request>();

            var actual = MemoryMarshal.Cast<Request, byte>(MemoryMarshal.CreateSpan(ref request, 1)).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ValidationIfIdInvalid()
        {
            uint invalidIdentifier = 100;
            var requestData = new byte[Marshal.SizeOf(typeof(Request))];
            BitConverter.GetBytes(invalidIdentifier).CopyTo(requestData, 0);
            BitConverter.GetBytes(ValidPd).CopyTo(requestData, 4);
            BitConverter.GetBytes(ValidType).CopyTo(requestData, 6);
            TestCncTime.AsSpan().ToArray().CopyTo(requestData, 8);

            var request = requestData.AsSpan().AsStruct<Request>();

            Assert.False(request.IsValid);
        }

        [Test]
        public void ValidationIfPdInvalid()
        {
            uint invalidPd = 100;
            var requestData = new byte[Marshal.SizeOf(typeof(Request))];
            BitConverter.GetBytes(ValidIdentifier).CopyTo(requestData, 0);
            BitConverter.GetBytes(invalidPd).CopyTo(requestData, 4);
            BitConverter.GetBytes(ValidType).CopyTo(requestData, 6);
            TestCncTime.AsSpan().ToArray().CopyTo(requestData, 8);

            var request = requestData.AsSpan().AsStruct<Request>();

            Assert.False(request.IsValid);
        }

        [Test]
        public void ValidationIfTypeInvalid()
        {
            uint invalidType = 100;
            var requestData = new byte[Marshal.SizeOf(typeof(Request))];
            BitConverter.GetBytes(ValidIdentifier).CopyTo(requestData, 0);
            BitConverter.GetBytes(ValidPd).CopyTo(requestData, 4);
            BitConverter.GetBytes(invalidType).CopyTo(requestData, 6);
            TestCncTime.AsSpan().ToArray().CopyTo(requestData, 8);

            var request = requestData.AsSpan().AsStruct<Request>();

            Assert.False(request.IsValid);
        }

        [Test]
        public void CheckConvertionFromSpanIfLengthLessThanStructSize()
        {
            var expected = new byte[Marshal.SizeOf(typeof(Request))];
            BitConverter.GetBytes(ValidIdentifier).CopyTo(expected, 0);
            BitConverter.GetBytes(ValidPd).CopyTo(expected, 4);
            BitConverter.GetBytes(ValidType).CopyTo(expected, 6);
            TestCncTime.AsSpan().ToArray().CopyTo(expected, 8);

            Assert.Throws(typeof(SpanLengthException), () => expected[..20].AsSpan().AsStruct<Request>());
        }

        [Test]
        public void CheckConvertionFromSpanIfLengthMoreThanStructSize()
        {
            var additionalSize = 10;
            var expected = new byte[Marshal.SizeOf(typeof(Request)) + additionalSize];
            BitConverter.GetBytes(ValidIdentifier).CopyTo(expected, 0);
            BitConverter.GetBytes(ValidPd).CopyTo(expected, 4);
            BitConverter.GetBytes(ValidType).CopyTo(expected, 6);
            TestCncTime.AsSpan().ToArray().CopyTo(expected, 8);

            Assert.Throws(typeof(SpanLengthException), () => expected.AsSpan().AsStruct<Request>());
        }

        [Test]
        public void CheckConvertionFromSpanIfSpanIsNull()
        {
            Assert.Throws(typeof(ArgumentNullException), () =>
            {
                Span<byte> nullSpan = null;
                nullSpan.AsStruct<Request>();
            });
        }
    }
}