using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketWriterTests
{
    public class WriteInt8Tests
    {
        [Fact]
        public void Write_Int8_One_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteInt8(1);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 1 });
            }
        }

        [Fact]
        public void Write_Int8_Multiple_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteInt8(1);
                writer.WriteInt8(2);
                writer.WriteInt8(3);
                writer.WriteInt8(4);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 1, 2, 3, 4 });
            }
        }
    }
}