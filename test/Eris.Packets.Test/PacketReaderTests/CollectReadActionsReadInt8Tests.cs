using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsReadInt8Tests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Collect_ReadInt8_Should_Have_One_Action()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadInt8();

                var result = reader.GetPacketReadActions();
                result.Should().HaveCount(1);
            }
        }

        [Fact]
        public void Collect_ReadInt8_Should_Have_Action_Count()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadInt8();

                var result = reader.GetPacketReadActions();
                result[0].Count.Should().Be(1);
            }
        }

        [Fact]
        public void Collect_ReadInt8_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadInt8("this is a message");

                var result = reader.GetPacketReadActions();
                result[0].Message.Should().Be("Int8: this is a message");
            }
        }
    }
}