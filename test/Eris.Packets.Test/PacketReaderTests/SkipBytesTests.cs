using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class SkipBytesTests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Skip_Two_Bytes()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(2);

                var result = reader.HasMore();
                result.Should().BeTrue();
            }
        }

        [Fact]
        public void Skip_Four_Bytes()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(4);

                var result = reader.HasMore();
                result.Should().BeFalse();
            }
        }

        [Fact]
        public void Skip_Bytes_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                Action action = () => reader.SkipBytes(5);

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}