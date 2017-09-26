using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadUInt8EnumTests
    {
        private enum OptionsWithoutInherentance
        {
            One = 1,
            Two = 2
        }

        private enum Options : byte
        {
            One = 1,
            Two = 2
        }

        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Read_UInt8Enum_Should_Be_Of_Type_Options()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt8<Options>();

                result.Should().BeOfType(typeof(Options));
            }
        }

        [Fact]
        public void Read_First_UInt8Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt8<Options>();

                result.Should().Be(Options.One);
            }
        }

        [Fact]
        public void Read_Second_UInt8Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadUInt8();
                var result = reader.ReadUInt8<Options>();

                result.Should().Be(Options.Two);
            }
        }

        [Fact]
        public void Read_UInt8Enum_Unknown_Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(2);
                var result = reader.ReadUInt8<Options>();

                Enum.IsDefined(typeof(Options), result).Should().BeFalse();
            }
        }

        [Fact]
        public void Read_UInt8Enum_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(4);

                Action action = () => reader.ReadUInt8<Options>();

                action.ShouldThrow<PacketReadException>();
            }
        }

        [Fact]
        public void Read_UInt8Enum_Invalid_Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                Action action = () => reader.ReadUInt8<OptionsWithoutInherentance>();

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}