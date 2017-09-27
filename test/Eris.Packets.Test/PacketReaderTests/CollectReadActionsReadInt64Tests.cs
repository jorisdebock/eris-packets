using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsReadInt64Tests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        [Fact]
        public void Collect_ReadInt64_Should_Have_One_Action()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadInt64();

                var result = reader.GetPacketReadActions();
                result.Should().HaveCount(1);
            }
        }

        [Fact]
        public void Collect_ReadInt64_Should_Have_Action_Count()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadInt64();

                var result = reader.GetPacketReadActions();
                result[0].Count.Should().Be(8);
            }
        }

        [Fact]
        public void Collect_ReadInt64_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadInt64("this is a message");

                var result = reader.GetPacketReadActions();
                result[0].Message.Should().Be("Int64: this is a message");
            }
        }
    }
}