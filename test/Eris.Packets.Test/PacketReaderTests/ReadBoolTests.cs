using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadBoolTests
    {
        private readonly byte[] _data = new byte[] { 0, 1, 2 };

        [Fact]
        public void Read_Bool_Zero_Should_Be_False()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadBool();

                result.Should().BeFalse();
            }
        }

        [Fact]
        public void Read_Bool_One_Should_Be_True()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadBool();
                var result = reader.ReadBool();

                result.Should().BeTrue();
            }
        }

        [Fact]
        public void Read_Bool_Anything_Other_Than_Zero_Should_Be_False()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(2);
                var result = reader.ReadBool();

                result.Should().BeFalse();
            }
        }

        [Fact]
        public void Read_Bool_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(3);

                Action action = () => reader.ReadBool();

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}