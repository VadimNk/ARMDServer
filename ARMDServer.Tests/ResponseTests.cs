using System;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace ARMDServer.Tests
{
    public class ResponseTests
    {
        private BinaryDateTime TestCncTime { get; set; }

        [SetUp]
        public void Setup()
        {
            TestCncTime = BinaryDateTime.FromDateTime(DateTime.Now - TimeSpan.FromHours(10));
        }

        [Test]
        public void AbsoluteSizeOfType()
        {
            var request = new Response();
            var expectedSize =
                Marshal.SizeOf(request.CncTime) +
                Marshal.SizeOf(request.StartUpTime) +
                Marshal.SizeOf(request.LocalTime) +
                Marshal.SizeOf(request.TicksFromStartUp);

            var actualSize = Marshal.SizeOf(request);

            Assert.AreEqual(expectedSize, actualSize);
        }

        [Test]
        public void CncTimeAreEqual()
        {
            var response = new Response(TestCncTime);

            Assert.AreEqual(response.CncTime, TestCncTime);
        }

        [Test]
        public void StartupTimeLessThenLocalTime()
        {
            var response = new Response(TestCncTime);

            Assert.IsTrue(response.StartUpTime.ToDateTime() < response.LocalTime.ToDateTime());
        }

        [Test]
        public void StructToArrayConvertion()
        {
            var response = new Response(TestCncTime);
            var starUpTime = response.StartUpTime;
            var LocalTime = response.LocalTime;
            var ticksFromStartUp = response.TicksFromStartUp;

            var expected = new byte[Marshal.SizeOf(response)];
            TestCncTime.AsSpan().ToArray().CopyTo(expected, 0);
            starUpTime.AsSpan().ToArray().CopyTo(expected, 16);
            LocalTime.AsSpan().ToArray().CopyTo(expected, 32);
            BitConverter.GetBytes(ticksFromStartUp).CopyTo(expected, 48);

            var actual = response.ToArray();

            CollectionAssert.AreEqual(expected, actual[..Marshal.SizeOf(response)]);
        }

        [Test]
        public void ChecksumAppending()
        {
            var response = new Response(TestCncTime);
            var starUpTime = response.StartUpTime;
            var LocalTime = response.LocalTime;
            var ticksFromStartUp = response.TicksFromStartUp;

            var partOfChecksum = new byte[Marshal.SizeOf(response)];
            TestCncTime.AsSpan().ToArray().CopyTo(partOfChecksum, 0);
            starUpTime.AsSpan().ToArray().CopyTo(partOfChecksum, 16);
            LocalTime.AsSpan().ToArray().CopyTo(partOfChecksum, 32);
            BitConverter.GetBytes(ticksFromStartUp).CopyTo(partOfChecksum, 48);
            var expected = Enumerable.Repeat(partOfChecksum, Response.ChecksumRepeats)
                .SelectMany(item => item);

            var actual = response.ToArray()[Marshal.SizeOf(response)..];

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertionToArray()
        {
            var response = new Response(TestCncTime);
            var starUpTime = response.StartUpTime;
            var LocalTime = response.LocalTime;
            var ticksFromStartUp = response.TicksFromStartUp;

            var partOfChecksum = new byte[Marshal.SizeOf(response)];
            TestCncTime.AsSpan().ToArray().CopyTo(partOfChecksum, 0);
            starUpTime.AsSpan().ToArray().CopyTo(partOfChecksum, 16);
            LocalTime.AsSpan().ToArray().CopyTo(partOfChecksum, 32);
            BitConverter.GetBytes(ticksFromStartUp).CopyTo(partOfChecksum, 48);
            var expected = Enumerable.Repeat(partOfChecksum, 1 + Response.ChecksumRepeats)
                .SelectMany(item => item);

            var actual = response.ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}