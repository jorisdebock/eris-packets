using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadUInt16Tests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Read_UInt16_Should_Be_Of_Type_UShort()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt16();

                result.Should().BeOfType(typeof(ushort));
            }
        }

        [Fact]
        public void Read_First_UInt16()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt16();

                result.Should().Be(0x0201);
            }
        }

        [Fact]
        public void Read_Second_UInt16()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadUInt16();
                var result = reader.ReadUInt16();

                result.Should().Be(0x0403);
            }
        }

        [Fact]
        public void Read_UInt16_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(4);

                Action action = () => reader.ReadUInt16();

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}