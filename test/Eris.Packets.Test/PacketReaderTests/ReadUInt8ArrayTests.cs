using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadUInt8ArrayTests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Read_UInt8Array_Should_Be_Of_Type_ByteArray()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt8Array(1);

                result.Should().BeOfType(typeof(byte[]));
            }
        }

        [Fact]
        public void Read_First_UInt8Array()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadUInt8Array(2);

                result.Should().Equal(new byte[] { 1, 2 });
            }
        }

        [Fact]
        public void Read_Second_UInt8Array()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(1);
                var result = reader.ReadUInt8Array(3);

                result.Should().Equal(new byte[] { 2, 3, 4 });
            }
        }

        [Fact]
        public void Read_UInt8Array_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                Action action = () => reader.ReadUInt8Array(5);

                action.ShouldThrow<PacketReadException>();
            }
        }
    }
}