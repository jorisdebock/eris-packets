using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsReadUnicodeTests
    {
        private readonly byte[] _data = new byte[] { 4, 0, (byte)'t', 0, (byte)'e', 0, (byte)'x', 0, (byte)'t', 0 };

        [Fact]
        public void Collect_ReadUnicode_Should_Have_Two_Action()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUnicode();

                var result = reader.GetPacketReadActions();
                result.Should().HaveCount(2);
            }
        }

        [Fact]
        public void Collect_ReadUnicode_Should_Have_Action_Length_Count()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUnicode();

                var result = reader.GetPacketReadActions();
                result[0].Count.Should().Be(2);
            }
        }

        [Fact]
        public void Collect_ReadUnicode_Should_Have_Action_Length_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUnicode("this is a message");

                var result = reader.GetPacketReadActions();
                result[0].Message.Should().Be("Unicode length (8): this is a message");
            }
        }

        [Fact]
        public void Collect_ReadUnicode_Should_Have_Action_Count()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUnicode();

                var result = reader.GetPacketReadActions();
                result[1].Count.Should().Be(8);
            }
        }

        [Fact]
        public void Collect_ReadUnicode_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUnicode("this is a message");

                var result = reader.GetPacketReadActions();
                result[1].Message.Should().Be("Unicode (\"text\"): this is a message");
            }
        }
    }
}