using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketWriterTests
{
    public class WriteBoolTests
    {
        [Fact]
        public void Write_Bool_True_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteBool(true);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 1 });
            }
        }

        [Fact]
        public void Write_Bool_False_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteBool(false);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 0 });
            }
        }

        [Fact]
        public void Write_Bool_Multiple_Value()
        {
            using (var writer = new PacketWriter())
            {
                writer.WriteBool(true);
                writer.WriteBool(true);
                writer.WriteBool(false);
                writer.WriteBool(true);

                var result = writer.GetBytes();

                result.Should().Equal(new byte[] { 1, 1, 0, 1 });
            }
        }
    }
}