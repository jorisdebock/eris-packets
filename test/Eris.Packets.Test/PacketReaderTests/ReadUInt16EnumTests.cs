using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadUInt16EnumTests
    {
        private enum OptionsWithoutInherentance
        {
            One = 0x0201,
            Two = 0x0403
        }

        private enum Options : ushort
        {
            One = 0x0201,
            Two = 0x0403
        }

        private readonly byte[] _data = new byte[] { 1, 2, 3, 4, 5, 6 };

        [Fact]
        public void Read_UInt16Enum_Should_Be_Of_Type_Options()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt16<Options>();

                result.Should().BeOfType(typeof(Options));
            }
        }

        [Fact]
        public void Read_First_UInt16Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt16<Options>();

                result.Should().Be(Options.One);
            }
        }

        [Fact]
        public void Read_Second_UInt16Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadUInt16();
                var result = reader.ReadUInt16<Options>();

                result.Should().Be(Options.Two);
            }
        }

        [Fact]
        public void Read_UInt16Enum_Unknown_Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(4);
                var result = reader.ReadUInt16<Options>();

                Enum.IsDefined(typeof(Options), result).Should().BeFalse();
            }
        }

        [Fact]
        public void Read_UInt16Enum_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(5);

                Action action = () => reader.ReadUInt16<Options>();

                action.ShouldThrow<PacketReadException>();
            }
        }

        [Fact]
        public void Read_UInt16Enum_Invalid_Enum()
        {
            using (var reader = new PacketReader(_data))
            {
                Action action = () => reader.ReadUInt16<OptionsWithoutInherentance>();

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}