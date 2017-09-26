using Eris.Extensions.Security;
using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketWriterTests
{
    public class WriteUnicodeTests
    {
        [Fact]
        public void Write_Unicode_One_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteUnicode("text");

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] {
                    4, 0, (byte)'t', 0, (byte)'e', 0, (byte)'x', 0, (byte)'t', 0 });
            }
        }

        [Fact]
        public void Write_Unicode_Multiple_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteUnicode("text");
                writer.WriteUnicode("text123");

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] {
                    4, 0, (byte)'t', 0, (byte)'e', 0, (byte)'x', 0, (byte)'t', 0,
                    7, 0, (byte)'t', 0, (byte)'e', 0, (byte)'x', 0, (byte)'t', 0, (byte)'1', 0, (byte)'2', 0, (byte)'3', 0  });
            }
        }
    }
}