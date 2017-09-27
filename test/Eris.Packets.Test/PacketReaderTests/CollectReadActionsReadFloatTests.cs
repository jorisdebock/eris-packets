using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsReadFloatTests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Collect_ReadFloat_Should_Have_One_Action()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadFloat();

                var result = reader.GetPacketReadActions();
                result.Should().HaveCount(1);
            }
        }

        [Fact]
        public void Collect_ReadFloat_Should_Have_Action_Count()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadFloat();

                var result = reader.GetPacketReadActions();
                result[0].Count.Should().Be(4);
            }
        }

        [Fact]
        public void Collect_ReadFloat_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadFloat("this is a message");

                var result = reader.GetPacketReadActions();
                result[0].Message.Should().Be("Float (1.53999E-36): this is a message");
            }
        }
    }
}