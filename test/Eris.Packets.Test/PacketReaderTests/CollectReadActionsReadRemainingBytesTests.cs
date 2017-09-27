using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsReadRemainingBytesTests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Collect_ReadRemainingBytes_Should_Have_One_Action()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadRemainingBytes();

                var result = reader.GetPacketReadActions();
                result.Should().HaveCount(1);
            }
        }

        [Fact]
        public void Collect_ReadRemainingBytes_Should_Have_Action_Count()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadRemainingBytes();

                var result = reader.GetPacketReadActions();
                result[0].Count.Should().Be(4);
            }
        }

        [Fact]
        public void Collect_ReadRemainingBytes_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadRemainingBytes("this is a message");

                var result = reader.GetPacketReadActions();
                result[0].Message.Should().Be("Remaining (4): this is a message");
            }
        }
    }
}