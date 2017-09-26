using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketWriterTests
{
    public class WriteUInt64Tests
    {
        [Fact]
        public void Write_UInt64_One_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt64(1);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 });
            }
        }

        [Fact]
        public void Write_UInt64_Multiple_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt64(0x0807060504030201);
                writer.WriteUInt64(0x100f0e0d0c0b0a09);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] {
                    0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                    0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x10 });
            }
        }
    }
}