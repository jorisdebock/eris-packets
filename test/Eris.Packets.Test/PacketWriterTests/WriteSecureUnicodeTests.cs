using Eris.Extensions.Security;
using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketWriterTests
{
    public class WriteSecureUnicodeTests
    {
        [Fact]
        public void Write_SecureUnicode_One_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteSecureUnicode("text".ToSecureString());

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] {
                    4, 0, (byte)'t', 0, (byte)'e', 0, (byte)'x', 0, (byte)'t', 0 });
            }
        }

        [Fact]
        public void Write_SecureUnicode_Multiple_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteSecureUnicode("text".ToSecureString());
                writer.WriteSecureUnicode("text123".ToSecureString());

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] {
                    4, 0, (byte)'t', 0, (byte)'e', 0, (byte)'x', 0, (byte)'t', 0,
                    7, 0, (byte)'t', 0, (byte)'e', 0, (byte)'x', 0, (byte)'t', 0, (byte)'1', 0, (byte)'2', 0, (byte)'3', 0  });
            }
        }
    }
}