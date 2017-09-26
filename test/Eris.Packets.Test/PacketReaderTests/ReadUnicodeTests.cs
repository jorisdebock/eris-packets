using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadUnicodeTests
    {
        private readonly byte[] _data = new byte[] {
            4, 0, (byte)'t', 0, (byte)'e', 0, (byte)'x', 0, (byte)'t', 0,
            7, 0, (byte)'t', 0, (byte)'e', 0, (byte)'x', 0, (byte)'t', 0, (byte)'1', 0, (byte)'2', 0, (byte)'3', 0 };

        [Fact]
        public void Read_Unicode_Should_Be_Of_Type_String()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUnicode();

                result.Should().BeOfType(typeof(string));
            }
        }

        [Fact]
        public void Read_First_Unicode()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUnicode();

                result.Should().Be("text");
            }
        }

        [Fact]
        public void Read_Second_Unicode()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadUnicode();
                var result = reader.ReadUnicode();

                result.Should().Be("text123");
            }
        }

        [Fact]
        public void Read_Unicode_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(2);

                Action action = () => reader.ReadUnicode();

                action.ShouldThrow<PacketReadException>();
            }
        }

        [Fact]
        public void Read_Length_For_Unicode_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(25);

                Action action = () => reader.ReadUnicode();

                action.ShouldThrow<PacketReadException>();
            }
        }

        [Fact]
        public void Read_Zero_Length_Should_Be_Null()
        {
            using (var reader = new PacketReader(new byte[] { 0, 0 }))
            {
                var result = reader.ReadUnicode();

                result.Should().BeNull();
            }
        }
    }
}