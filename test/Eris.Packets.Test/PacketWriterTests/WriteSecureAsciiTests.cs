using Eris.Extensions.Security;
using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketWriterTests
{
    public class WriteSecureAsciiTests
    {
        [Fact]
        public void Write_SecureAscii_One_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteSecureAscii("text".ToSecureString());

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] {
                    4, 0, (byte)'t', (byte)'e', (byte)'x', (byte)'t' });
            }
        }

        [Fact]
        public void Write_SecureAscii_Multiple_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteSecureAscii("text".ToSecureString());
                writer.WriteSecureAscii("text123".ToSecureString());

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] {
                    4, 0, (byte)'t', (byte)'e', (byte)'x', (byte)'t',
                    7, 0, (byte)'t', (byte)'e', (byte)'x', (byte)'t', (byte)'1', (byte)'2', (byte)'3' });
            }
        }
    }
}