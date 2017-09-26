using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadUInt8Tests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Read_UInt8_Should_Be_Of_Type_Byte()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt8();

                result.Should().BeOfType(typeof(byte));
            }
        }

        [Fact]
        public void Read_First_UInt8()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt8();

                result.Should().Be(1);
            }
        }

        [Fact]
        public void Read_Second_UInt8()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadUInt8();
                var result = reader.ReadUInt8();

                result.Should().Be(2);
            }
        }

        [Fact]
        public void Read_UInt8_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(4);

                Action action = () => reader.ReadUInt8();

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}