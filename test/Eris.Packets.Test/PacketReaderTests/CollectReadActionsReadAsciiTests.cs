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
                result.Actions.Should().HaveCount(2);
            }
        }

        [Fact]
        public void Collect_ReadAscii_Should_Have_Action_Length_ReadCount()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadAscii();

                var result = reader.GetPacketReadActions();
                result.Actions[0].ReadCount.Should().Be(2);
            }
        }

        [Fact]
        public void Collect_ReadAscii_Should_Have_Action_Length_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadAscii("this is a message");

                var result = reader.GetPacketReadActions();
                result.Actions[0].Message.Should().Be("Ascii length (4): this is a message");
            }
        }

        [Fact]
        public void Collect_ReadAscii_Should_Have_Action_ReadCount()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadAscii();

                var result = reader.GetPacketReadActions();
                result.Actions[1].ReadCount.Should().Be(4);
            }
        }

        [Fact]
        public void Collect_ReadAscii_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadAscii("this is a message");

                var result = reader.GetPacketReadActions();
                result.Actions[1].Message.Should().Be("Ascii (\"text\"): this is a message");
            }
        }
    }
}