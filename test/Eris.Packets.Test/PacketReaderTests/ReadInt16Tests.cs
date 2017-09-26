using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadInt16Tests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Read_Int16_Should_Be_Of_Type_Short()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadInt16();

                result.Should().BeOfType(typeof(short));
            }
        }

        [Fact]
        public void Read_First_Int16()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadInt16();

                result.Should().Be(0x0201);
            }
        }

        [Fact]
        public void Read_Second_Int16()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadInt16();
                var result = reader.ReadInt16();

                result.Should().Be(0x0403);
            }
        }

        [Fact]
        public void Read_Int16_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(4);

                Action action = () => reader.ReadInt16();

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}