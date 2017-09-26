using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadFloatTests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        [Fact]
        public void Read_Float_Should_Be_Of_Type_Float()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadFloat();

                result.Should().BeOfType(typeof(float));
            }
        }

        [Fact]
        public void Read_First_Float()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadFloat();

                result.Should().BeApproximately(1.53999e-36f, .00001e-36f);
            }
        }

        [Fact]
        public void Read_Second_Float()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(4);
                var result = reader.ReadFloat();

                result.Should().BeApproximately(4.06322e-34f, .00001e-34f);
            }
        }

        [Fact]
        public void Read_Float_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(6);

                Action action = () => reader.ReadFloat();

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}