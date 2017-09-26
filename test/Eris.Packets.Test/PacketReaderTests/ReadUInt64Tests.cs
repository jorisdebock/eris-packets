using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadUInt64Tests
    {
        private readonly byte[] _data = new byte[] {
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
            0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x10};

        [Fact]
        public void Read_UInt64_Should_Be_Of_Type_ULong()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt64();

                result.Should().BeOfType(typeof(ulong));
            }
        }

        [Fact]
        public void Read_First_UInt64()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt64();

                result.Should().Be(0x0807_0605_0403_0201);
            }
        }

        [Fact]
        public void Read_Second_UInt64()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadUInt64();
                var result = reader.ReadUInt64();

                result.Should().Be(0x100f_0e0d_0c0b_0a09);
            }
        }

        [Fact]
        public void Read_UInt64_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(16);

                Action action = () => reader.ReadUInt64();

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}