using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketWriterTests
{
    public class WriteSeekTests
    {
        [Fact]
        public void Seek_Begin()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt8(1);

                writer.Seek(0, System.IO.SeekOrigin.Begin);

                writer.WriteUInt8(2);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 2 });
            }
        }

        [Fact]
        public void Seek_Current()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt8(1);

                writer.Seek(-1, System.IO.SeekOrigin.Current);

                writer.WriteUInt8(2);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 2 });
            }
        }

        [Fact]
        public void Seek_End()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteUInt8(1);

                writer.Seek(-1, System.IO.SeekOrigin.End);

                writer.WriteUInt8(2);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 2 });
            }
        }
    }
}