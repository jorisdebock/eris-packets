using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadAsciiTests
    {
        private readonly byte[] _data = new byte[] {
            4, 0, (byte)'t', (byte)'e', (byte)'x', (byte)'t',
            7, 0, (byte)'t', (byte)'e', (byte)'x', (byte)'t', (byte)'1', (byte)'2', (byte)'3' };

        [Fact]
        public void Read_Ascii_Should_Be_Of_Type_String()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadAscii();

                result.Should().BeOfType(typeof(string));
            }
        }

        [Fact]
        public void Read_First_Ascii()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadAscii();

                result.Should().Be("text");
            }
        }

        [Fact]
        public void Read_Second_Ascii()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadAscii();
                var result = reader.ReadAscii();

                result.Should().Be("text123");
            }
        }

        [Fact]
        public void Read_Ascii_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(2);

                Action action = () => reader.ReadAscii();

                action.ShouldThrow<PacketReadException>();
            }
        }

        [Fact]
        public void Read_Length_For_Ascii_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(14);

                Action action = () => reader.ReadAscii();

                action.ShouldThrow<PacketReadException>();
            }
        }

        [Fact]
        public void Read_Zero_Length_Should_Be_Null()
        {
            using (var reader = new PacketReader(new byte[] { 0, 0 }))
            {
                var result = reader.ReadAscii();

                result.Should().BeNull();
            }
        }
    }
}