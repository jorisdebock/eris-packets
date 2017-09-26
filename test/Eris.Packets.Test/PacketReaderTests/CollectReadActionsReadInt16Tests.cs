﻿using FluentAssertions;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsReadInt16Tests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void Collect_ReadInt16_Should_Have_One_Action()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadInt16();

                var result = reader.GetPacketReadActions();
                result.Actions.Should().HaveCount(1);
            }
        }

        [Fact]
        public void Collect_ReadInt16_Should_Have_Action_ReadCount()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadInt16();

                var result = reader.GetPacketReadActions();
                result.Actions[0].ReadCount.Should().Be(2);
            }
        }

        [Fact]
        public void Collect_ReadInt16_Should_Have_Action_Message()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.ReadInt16("this is a message");

                var result = reader.GetPacketReadActions();
                result.Actions[0].Message.Should().Be("Int16: this is a message");
            }
        }
    }
}