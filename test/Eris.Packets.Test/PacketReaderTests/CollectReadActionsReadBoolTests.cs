using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsReadBoolTests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Collect_ReadBool_Should_Have_One_Action()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadBool();

                var result = reader.GetPacketReadActions();
                result.Actions.Should().HaveCount(1);
            }
        }

        [Fact]
        public void Collect_ReadBool_Should_Have_Action_ReadCount()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadBool();

                var result = reader.GetPacketReadActions();
                result.Actions[0].ReadCount.Should().Be(1);
            }
        }

        [Fact]
        public void Collect_ReadBool_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadBool("this is a message");

                var result = reader.GetPacketReadActions();
                result.Actions[0].Message.Should().Be("Bool (True): this is a message");
            }
        }
    }
}