using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadInt32Tests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        [Fact]
        public void Read_Int32_Should_Be_Of_Type_Int()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadInt32();

                result.Should().BeOfType(typeof(int));
            }
        }

        [Fact]
        public void Read_First_Int32()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadInt32();

                result.Should().Be(0x0403_0201);
            }
        }

        [Fact]
        public void Read_Second_Int32()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadInt32();
                var result = reader.ReadInt32();

                result.Should().Be(0x0807_0605);
            }
        }

        [Fact]
        public void Read_Int32_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(8);

                Action action = () => reader.ReadInt32();

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}