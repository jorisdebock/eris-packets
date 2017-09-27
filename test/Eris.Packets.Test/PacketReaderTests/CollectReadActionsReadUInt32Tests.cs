using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsReadUInt32Tests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Collect_ReadUInt32_Should_Have_One_Action()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUInt32();

                var result = reader.GetPacketReadActions();
                result.Should().HaveCount(1);
            }
        }

        [Fact]
        public void Collect_ReadUInt32_Should_Have_Action_Count()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUInt32();

                var result = reader.GetPacketReadActions();
                result[0].Count.Should().Be(4);
            }
        }

        [Fact]
        public void Collect_ReadUInt32_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUInt32("this is a message");

                var result = reader.GetPacketReadActions();
                result[0].Message.Should().Be("UInt32: this is a message");
            }
        }
    }
}