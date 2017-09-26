using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsReadUInt8EnumTests
    {
        private enum Options : byte
        {
            One = 1,
            Two = 2
        }

        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Collect_ReadUInt8Enum_Should_Have_One_Action()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUInt8<Options>();

                var result = reader.GetPacketReadActions();
                result.Actions.Should().HaveCount(1);
            }
        }

        [Fact]
        public void Collect_ReadUInt8Enum_Should_Have_Action_ReadCount()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUInt8<Options>("this is a message");

                var result = reader.GetPacketReadActions();
                result.Actions[0].ReadCount.Should().Be(1);
            }
        }

        [Fact]
        public void Collect_ReadUInt8Enum_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUInt8<Options>("this is a message");

                var result = reader.GetPacketReadActions();
                result.Actions[0].Message.Should().Be("Enum8 (One): this is a message");
            }
        }
    }
}