using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadInt8Tests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Read_Int8_Should_Be_Of_Type_SByte()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadInt8();

                result.Should().BeOfType(typeof(sbyte));
            }
        }

        [Fact]
        public void Read_First_Int8()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadInt8();

                result.Should().Be(1);
            }
        }

        [Fact]
        public void Read_Second_Int8()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadInt8();
                var result = reader.ReadInt8();

                result.Should().Be(2);
            }
        }

        [Fact]
        public void Read_Int8_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(4);

                Action action = () => reader.ReadInt8();

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}