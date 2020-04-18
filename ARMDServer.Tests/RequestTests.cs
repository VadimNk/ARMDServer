using System;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace ARMDServer.Tests
{
    public class RequestTests
    {
        private const uint ValidIdentifier = 0x42535253;
        private const long ValidPd = 1;
        private const long ValidType = 1;

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
            BitConverter.GetBytes(ValidType).CopyTo(expected, 12);
            TestCncTime.AsSpan().ToArray().CopyTo(expected, 20);

            var request = Request.FromSpan(expected);

            var actual = MemoryMarshal.Cast<Request, byte>(MemoryMarshal.CreateSpan(ref request, 1)).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ValidationIfIdInvalid()
        {
            uint invalidIdentifier = 100;
            var requetData = new byte[Marshal.SizeOf(typeof(Request))];
            BitConverter.GetBytes(invalidIdentifier).CopyTo(requetData, 0);
            BitConverter.GetBytes(ValidPd).CopyTo(requetData, 4);
            BitConverter.GetBytes(ValidType).CopyTo(requetData, 12);
            TestCncTime.AsSpan().ToArray().CopyTo(requetData, 20);

            var request = Request.FromSpan(requetData);

            Assert.False(request.IsValid);
        }

        [Test]
        public void ValidationIfPdInvalid()
        {
            uint invalidPd = 100;
            var requetData = new byte[Marshal.SizeOf(typeof(Request))];
            BitConverter.GetBytes(ValidIdentifier).CopyTo(requetData, 0);
            BitConverter.GetBytes(invalidPd).CopyTo(requetData, 4);
            BitConverter.GetBytes(ValidType).CopyTo(requetData, 12);
            TestCncTime.AsSpan().ToArray().CopyTo(requetData, 20);

            var request = Request.FromSpan(requetData);

            Assert.False(request.IsValid);
        }

        [Test]
        public void ValidationIfTypeInvalid()
        {
            uint invalidType = 100;
            var requetData = new byte[Marshal.SizeOf(typeof(Request))];
            BitConverter.GetBytes(ValidIdentifier).CopyTo(requetData, 0);
            BitConverter.GetBytes(ValidPd).CopyTo(requetData, 4);
            BitConverter.GetBytes(invalidType).CopyTo(requetData, 12);
            TestCncTime.AsSpan().ToArray().CopyTo(requetData, 20);

            var request = Request.FromSpan(requetData);

            Assert.False(request.IsValid);
        }

        [Test]
        public void CheckConvertionFromSpanIfLengthLessThanStructSize()
        {
            var expected = new byte[Marshal.SizeOf(typeof(Request))];
            BitConverter.GetBytes(ValidIdentifier).CopyTo(expected, 0);
            BitConverter.GetBytes(ValidPd).CopyTo(expected, 4);
            BitConverter.GetBytes(ValidType).CopyTo(expected, 12);
            TestCncTime.AsSpan().ToArray().CopyTo(expected, 20);

            Assert.Throws(typeof(RequestLengthException), () => Request.FromSpan(expected[..20]));
        }

        [Test]
        public void CheckConvertionFromSpanIfLengthMoreThanStructSize()
        {
            var additionalSize = 10;
            var expected = new byte[Marshal.SizeOf(typeof(Request)) + additionalSize];
            BitConverter.GetBytes(ValidIdentifier).CopyTo(expected, 0);
            BitConverter.GetBytes(ValidPd).CopyTo(expected, 4);
            BitConverter.GetBytes(ValidType).CopyTo(expected, 12);
            TestCncTime.AsSpan().ToArray().CopyTo(expected, 20);

            Assert.Throws(typeof(RequestLengthException), () => Request.FromSpan(expected));
        }

        [Test]
        public void CheckConvertionFromSpanIfSpanIsNull()
        {
            Assert.Throws(typeof(ArgumentException), () => Request.FromSpan(null));
        }
    }
}