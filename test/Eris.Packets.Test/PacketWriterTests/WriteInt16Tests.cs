using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketWriterTests
{
    public class WriteInt16Tests
    {
        [Fact]
        public void Write_Int16_One_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteInt16(1);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 1, 0 });
            }
        }

        [Fact]
        public void Write_Int16_Multiple_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteInt16(0x0201);
                writer.WriteInt16(0x0403);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 1, 2, 3, 4 });
            }
        }
    }
}