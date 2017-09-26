using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadUInt64EnumTests
    {
        private enum OptionsWithoutInherentance : long
        {
            One = 0x0807060504030201,
            Two = 0x100f0e0d0c0b0a09
        }

        private enum Options : ulong
        {
            One = 0x0807060504030201,
            Two = 0x100f0e0d0c0b0a09
        }

        private readonly byte[] _data = new byte[] {
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
            0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x10};

        [Fact]
        public void Read_UInt64Enum_Should_Be_Of_Type_Options()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt64<Options>();

                result.Should().BeOfType(typeof(Options));
            }
        }

        [Fact]
        public void Read_First_UInt64Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt64<Options>();

                result.Should().Be(Options.One);
            }
        }

        [Fact]
        public void Read_Second_UInt64Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadUInt64();
                var result = reader.ReadUInt64<Options>();

                result.Should().Be(Options.Two);
            }
        }

        [Fact]
        public void Read_UInt64Enum_Unknown_Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(2);
                var result = reader.ReadUInt64<Options>();

                Enum.IsDefined(typeof(Options), result).Should().BeFalse();
            }
        }

        [Fact]
        public void Read_UInt64Enum_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(15);

                Action action = () => reader.ReadUInt64<Options>();

                action.ShouldThrow<PacketReadException>();
            }
        }

        [Fact]
        public void Read_UInt64Enum_Invalid_Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                Action action = () => reader.ReadUInt64<OptionsWithoutInherentance>();

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}