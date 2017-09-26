using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadRemainingBytesTests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Read_RemaingBytes_Should_Be_Of_Type_ByteArray()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadRemainingBytes();

                result.Should().BeOfType(typeof(byte[]));
            }
        }

        [Fact]
        public void Read_Remaining_Bytes_From_Start()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadRemainingBytes();

                result.Should().Equal(new byte[] { 1, 2, 3, 4 });
            }
        }

        [Fact]
        public void Read_Remaining_Bytes_From_Middle()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(2);

                var result = reader.ReadRemainingBytes();

                result.Should().Equal(new byte[] { 3, 4 });
            }
        }

        [Fact]
        public void Read_Remaining_Bytes_From_End()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(4);

                var result = reader.ReadRemainingBytes();

                result.Should().Equal(new byte[] { });
            }
        }
    }
}