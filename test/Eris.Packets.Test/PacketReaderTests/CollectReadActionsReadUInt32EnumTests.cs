using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsReadUInt32EnumTests
    {
        private enum Options : uint
        {
            One = 0x04030201,
            Two = 0x08070605
        }

        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Collect_ReadUInt32Enum_Should_Have_One_Action()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUInt32<Options>("this is a message");

                var result = reader.GetPacketReadActions();
                result.Should().HaveCount(1);
            }
        }

        [Fact]
        public void Collect_ReadUInt32Enum_Should_Have_Action_Count()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUInt32<Options>("this is a message");

                var result = reader.GetPacketReadActions();
                result[0].Count.Should().Be(4);
            }
        }

        [Fact]
        public void Collect_ReadUInt32Enum_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadUInt32<Options>("this is a message");

                var result = reader.GetPacketReadActions();
                result[0].Message.Should().Be("Enum32 (One): this is a message");
            }
        }
    }
}