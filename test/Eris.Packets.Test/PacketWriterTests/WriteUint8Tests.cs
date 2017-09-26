using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketWriterTests
{
    public class WriteUInt8Tests
    {
        [Fact]
        public void Write_UInt8_One_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt8(1);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 1 });
            }
        }

        [Fact]
        public void Write_UInt8_Multiple_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt8(1);
                writer.WriteUInt8(2);
                writer.WriteUInt8(3);
                writer.WriteUInt8(4);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 1, 2, 3, 4 });
            }
        }
    }
}