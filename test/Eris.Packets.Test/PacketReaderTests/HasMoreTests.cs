using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class HasMoreTests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void HasMore_Without_Reads()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.HasMore();

                result.Should().BeTrue();
            }
        }

        [Fact]
        public void HasMore_With_Reads()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadUInt16();
                var result = reader.HasMore();

                result.Should().BeTrue();
            }
        }

        [Fact]
        public void HasMore_With_Reads_Till_End()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadUInt32();
                var result = reader.HasMore();

                result.Should().BeFalse();
            }
        }
    }
}