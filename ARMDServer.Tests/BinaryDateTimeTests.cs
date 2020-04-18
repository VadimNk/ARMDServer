using NUnit.Framework;
using System;
using System.Runtime.InteropServices;

namespace ARMDServer.Tests
{
    public class BinaryDateTimeTests
    {
        private DateTime TestTime { get; set; }

        [SetUp]
        public void Setup()
        {
            TestTime = DateTime.Now;
        }

        [Test]
        public void AbsoluteSizeOfType()
        {
            var time = new BinaryDateTime();
            var expectedSize =
                Marshal.SizeOf(time.Year) +
                Marshal.SizeOf(time.Month) +
                Marshal.SizeOf(time.Day) +
                Marshal.SizeOf(time.Hour) +
                Marshal.SizeOf(time.Minute) +
                Marshal.SizeOf(time.Second) +
                Marshal.SizeOf(time.DayOfWeek) +
                Marshal.SizeOf(time.Millisecond);

            var actualSize = Marshal.SizeOf(time);

            Assert.AreEqual(expectedSize, actualSize);
        }

        [Test]
        public void ConvertionFromDateTime()
        {
            var time = BinaryDateTime.FromDateTime(TestTime);

            Assert.AreEqual(TestTime.Year, time.Year);
            Assert.AreEqual(TestTime.Month, time.Month);
            Assert.AreEqual(TestTime.Day, time.Day);
            Assert.AreEqual(TestTime.Hour, time.Hour);
            Assert.AreEqual(TestTime.Minute, time.Minute);
            Assert.AreEqual(TestTime.Second, time.Second);
            Assert.AreEqual((ushort)TestTime.DayOfWeek, time.DayOfWeek);
            Assert.AreEqual(TestTime.Millisecond, time.Millisecond);
        }

        [Test]
        public void ConvertionToSpanFromDateTime()
        {
            var expected = new byte[Marshal.SizeOf(new BinaryDateTime())];
            BitConverter.GetBytes((ushort)TestTime.Year).CopyTo(expected, 0);
            BitConverter.GetBytes((ushort)TestTime.Month).CopyTo(expected, 2);
            BitConverter.GetBytes((ushort)TestTime.DayOfWeek).CopyTo(expected, 4);
            BitConverter.GetBytes((ushort)TestTime.Day).CopyTo(expected, 6);
            BitConverter.GetBytes((ushort)TestTime.Hour).CopyTo(expected, 8);
            BitConverter.GetBytes((ushort)TestTime.Minute).CopyTo(expected, 10);
            BitConverter.GetBytes((ushort)TestTime.Second).CopyTo(expected, 12);
            BitConverter.GetBytes((ushort)TestTime.Millisecond).CopyTo(expected, 14);

            var time = BinaryDateTime.FromDateTime(TestTime);
            var actual = time.AsSpan().ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertionFromSpan()
        {
            var expected = new byte[16];
            BitConverter.GetBytes((ushort)TestTime.Year).CopyTo(expected, 0);
            BitConverter.GetBytes((ushort)TestTime.Month).CopyTo(expected, 2);
            BitConverter.GetBytes((ushort)TestTime.DayOfWeek).CopyTo(expected, 4);
            BitConverter.GetBytes((ushort)TestTime.Day).CopyTo(expected, 6);
            BitConverter.GetBytes((ushort)TestTime.Hour).CopyTo(expected, 8);
            BitConverter.GetBytes((ushort)TestTime.Minute).CopyTo(expected, 10);
            BitConverter.GetBytes((ushort)TestTime.Second).CopyTo(expected, 12);
            BitConverter.GetBytes((ushort)TestTime.Millisecond).CopyTo(expected, 14);

            var time = BinaryDateTime.FromSpan(expected);

            Assert.AreEqual(TestTime.Year, time.Year);
            Assert.AreEqual(TestTime.Month, time.Month);
            Assert.AreEqual(TestTime.Day, time.Day);
            Assert.AreEqual(TestTime.Hour, time.Hour);
            Assert.AreEqual(TestTime.Minute, time.Minute);
            Assert.AreEqual(TestTime.Second, time.Second);
            Assert.AreEqual((ushort)TestTime.DayOfWeek, time.DayOfWeek);
            Assert.AreEqual(TestTime.Millisecond, time.Millisecond);
        }

        [Test]
        public void ConvertionFromSpanToSpan()
        {
            var expected = new byte[16];
            BitConverter.GetBytes((ushort)TestTime.Year).CopyTo(expected, 0);
            BitConverter.GetBytes((ushort)TestTime.Month).CopyTo(expected, 2);
            BitConverter.GetBytes((ushort)TestTime.DayOfWeek).CopyTo(expected, 4);
            BitConverter.GetBytes((ushort)TestTime.Day).CopyTo(expected, 6);
            BitConverter.GetBytes((ushort)TestTime.Hour).CopyTo(expected, 8);
            BitConverter.GetBytes((ushort)TestTime.Minute).CopyTo(expected, 10);
            BitConverter.GetBytes((ushort)TestTime.Second).CopyTo(expected, 12);
            BitConverter.GetBytes((ushort)TestTime.Millisecond).CopyTo(expected, 14);

            var time = BinaryDateTime.FromSpan(expected);
            var actual = time.AsSpan().ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}