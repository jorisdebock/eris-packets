using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsReadAsciiTests
    {
        private readonly byte[] _data = new byte[] { 4, 0, (byte)'t', (byte)'e', (byte)'x', (byte)'t' };

        [Fact]
        public void Collect_ReadAscii_Should_Have_Two_Action()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadAscii();

                var result = reader.GetPacketReadActions();
                result.Should().HaveCount(2);
            }
        }

        [Fact]
        public void Collect_ReadAscii_Should_Have_Action_Length_Count()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadAscii();

                var result = reader.GetPacketReadActions();
                result[0].Count.Should().Be(2);
            }
        }

        [Fact]
        public void Collect_ReadAscii_Should_Have_Action_Length_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadAscii("this is a message");

                var result = reader.GetPacketReadActions();
                result[0].Message.Should().Be("Ascii length (4): this is a message");
            }
        }

        [Fact]
        public void Collect_ReadAscii_Should_Have_Action_Count()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadAscii();

                var result = reader.GetPacketReadActions();
                result[1].Count.Should().Be(4);
            }
        }

        [Fact]
        public void Collect_ReadAscii_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadAscii("this is a message");

                var result = reader.GetPacketReadActions();
                result[1].Message.Should().Be("Ascii (\"text\"): this is a message");
            }
        }
    }
}