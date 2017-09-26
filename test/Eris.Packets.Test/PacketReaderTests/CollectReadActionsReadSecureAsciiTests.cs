using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsReadSecureAsciiTests
    {
        private readonly byte[] _data = new byte[] { 4, 0, (byte)'t', (byte)'e', (byte)'x', (byte)'t' };

        [Fact]
        public void Collect_ReadSecureAscii_Should_Have_Two_Action()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadSecureAscii();

                var result = reader.GetPacketReadActions();
                result.Actions.Should().HaveCount(2);
            }
        }

        [Fact]
        public void Collect_ReadSecureAscii_Should_Have_Action_Length_ReadCount()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadSecureAscii();

                var result = reader.GetPacketReadActions();
                result.Actions[0].ReadCount.Should().Be(2);
            }
        }

        [Fact]
        public void Collect_ReadSecureAscii_Should_Have_Action_Length_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadSecureAscii("this is a message");

                var result = reader.GetPacketReadActions();
                result.Actions[0].Message.Should().Be("Ascii length (4): this is a message");
            }
        }

        [Fact]
        public void Collect_ReadSecureAscii_Should_Have_Action_ReadCount()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadSecureAscii();

                var result = reader.GetPacketReadActions();
                result.Actions[1].ReadCount.Should().Be(4);
            }
        }

        [Fact]
        public void Collect_ReadSecureAscii_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadSecureAscii("this is a message");

                var result = reader.GetPacketReadActions();
                result.Actions[1].Message.Should().Be("Ascii (\"********\"): this is a message");
            }
        }
    }
}