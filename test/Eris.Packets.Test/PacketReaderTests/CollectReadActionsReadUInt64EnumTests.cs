using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsReadUInt64EnumTests
    {
        private enum Options : ulong
        {
            One = 0x0807060504030201,
            Two = 0x100f0e0d0c0b0a09
        }

        private readonly byte[] _data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        [Fact]
        public void Collect_ReadUInt64Enum_Should_Have_One_Action()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUInt64<Options>("this is a message");

                var result = reader.GetPacketReadActions();
                result.Should().HaveCount(1);
            }
        }

        [Fact]
        public void Collect_ReadUInt64Enum_Should_Have_Action_Count()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUInt64<Options>("this is a message");

                var result = reader.GetPacketReadActions();
                result[0].Count.Should().Be(8);
            }
        }

        [Fact]
        public void Collect_ReadUInt64Enum_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUInt64<Options>("this is a message");

                var result = reader.GetPacketReadActions();
                result[0].Message.Should().Be("Enum64 (One): this is a message");
            }
        }
    }
}