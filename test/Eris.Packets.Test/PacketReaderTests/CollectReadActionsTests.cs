using FluentAssertions;
using System;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class CollectReadActionsTests
    {
        private readonly byte[] _data = new byte[] { 1, 2, 3, 4 };

        [Fact]
        public void No_Collect_Should_Have_No_PacketReadActions()
        {
            using (var reader = new PacketReader(_data, collectReadActions: false))
            {
                reader.ReadUInt8();

                var result = reader.GetPacketReadActions();
                result.Should().BeNull();
            }
        }

        [Fact]
        public void Collect_Should_Have_PacketReadActions()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.SkipBytes(2);

                var result = reader.GetPacketReadActions();
                result.Should().NotBeNull();
            }
        }

        [Fact]
        public void Collect_Should_Have_Actions()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.SkipBytes(2);

                var result = reader.GetPacketReadActions();
                result.Actions.Should().NotBeNullOrEmpty();
            }
        }

        [Fact]
        public void Collect_Should_Have_Data()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.SkipBytes(2);

                var result = reader.GetPacketReadActions();
                result.Data.Should().NotBeNullOrEmpty();
            }
        }

        [Fact]
        public void Collect_Should_Have_Data_That_Is_The_Same_As_The_Input_Data()
        {
            using (var reader = new PacketReader(_data, collectReadActions: true))
            {
                reader.SkipBytes(2);

                var result = reader.GetPacketReadActions();
                result.Data.Should().BeSameAs(_data);
            }
        }
    }
}