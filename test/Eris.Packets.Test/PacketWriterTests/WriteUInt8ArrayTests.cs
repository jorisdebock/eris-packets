using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketWriterTests
{
    public class WriteUInt8ArrayTests
    {
        [Fact]
        public void Write_UInt8Array_One_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt8Array(new byte[] { 1, 2, 3, 4 });

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 1, 2, 3, 4 });
            }
        }

        [Fact]
        public void Write_UInt8Array_Multiple_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt8Array(new byte[] { 1, 2, 3, 4 });
                writer.WriteUInt8Array(new byte[] { 5, 6, 7 });

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 1, 2, 3, 4, 5, 6, 7 });
            }
        }
    }
}