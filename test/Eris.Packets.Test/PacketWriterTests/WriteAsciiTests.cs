using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketWriterTests
{
    public class WriteAsciiTests
    {
        [Fact]
        public void Write_Ascii_One_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteAscii("text");

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] {
                    4, 0, (byte)'t', (byte)'e', (byte)'x', (byte)'t' });
            }
        }

        [Fact]
        public void Write_Ascii_Multiple_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteAscii("text");
                writer.WriteAscii("text123");

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] {
                    4, 0, (byte)'t', (byte)'e', (byte)'x', (byte)'t',
                    7, 0, (byte)'t', (byte)'e', (byte)'x', (byte)'t', (byte)'1', (byte)'2', (byte)'3' });
            }
        }
    }
}