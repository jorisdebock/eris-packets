using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadUInt32EnumTests
    {
        private enum OptionsWithoutInherentance
        {
            One = 0x04030201,
            Two = 0x08070605
        }

        private enum Options : uint
        {
            One = 0x04030201,
            Two = 0x08070605
        }

        private readonly byte[] _data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        [Fact]
        public void Read_UInt32Enum_Should_Be_Of_Type_Options()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt32<Options>();

                result.Should().BeOfType(typeof(Options));
            }
        }

        [Fact]
        public void Read_First_UInt32Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt32<Options>();

                result.Should().Be(Options.One);
            }
        }

        [Fact]
        public void Read_Second_UInt32Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadUInt32();
                var result = reader.ReadUInt32<Options>();

                result.Should().Be(Options.Two);
            }
        }

        [Fact]
        public void Read_UInt32Enum_Unknown_Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(2);
                var result = reader.ReadUInt32<Options>();

                Enum.IsDefined(typeof(Options), result).Should().BeFalse();
            }
        }

        [Fact]
        public void Read_UInt32Enum_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(7);

                Action action = () => reader.ReadUInt32<Options>();

                action.ShouldThrow<PacketReadException>();
            }
        }

        [Fact]
        public void Read_UInt32Enum_Invalid_Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                Action action = () => reader.ReadUInt32<OptionsWithoutInherentance>();

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}