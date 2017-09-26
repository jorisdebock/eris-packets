using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketWriterTests
{
    public class WriteUInt32Tests
    {
        [Fact]
        public void Write_UInt32_One_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt32(1);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 1, 0, 0, 0 });
            }
        }

        [Fact]
        public void Write_UInt32_Multiple_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt32(0x04030201);
                writer.WriteUInt32(0x08070605);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            }
        }
    }
}