using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketWriterTests
{
    public class WriteFloatTests
    {
        [Fact]
        public void Write_Float_One_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteFloat(1f);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 0, 0, 0x80, 0x3f });
            }
        }

        [Fact]
        public void Write_Float_Multiple_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteFloat(-100.156f);
                writer.WriteFloat(5.123f);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 0xDF, 0x4F, 0xC8, 0xC2, 0x9E, 0xEF, 0xA3, 0x40 });
            }
        }
    }
}