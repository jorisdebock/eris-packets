using Eris.Extensions.Security;
using FluentAssertions;
using System;
using System.Security;
using System.Text;
using Xunit;

namespace Eris.Packets.Test.PacketReaderTests
{
    public class ReadSecureAsciiTests
    {
        private readonly byte[] _data = new byte[] {
            4, 0, (byte)'t', (byte)'e', (byte)'x', (byte)'t',
            7, 0, (byte)'t', (byte)'e', (byte)'x', (byte)'t', (byte)'1', (byte)'2', (byte)'3' };

        [Fact]
        public void Read_SecureAscii_Should_Be_Of_Type_String()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadSecureAscii();

                result.Should().BeOfType(typeof(SecureString));
            }
        }

        [Fact]
        public void Read_First_SecureAscii()
        {
            using (var reader = new PacketReader(_data))
            {
                var result = reader.ReadSecureAscii();

                result.GetString(Encoding.ASCII).Should().Be("text");
            }
        }

        [Fact]
        public void Read_Second_SecureAscii()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.ReadSecureAscii();
                var result = reader.ReadSecureAscii();

                result.GetString(Encoding.ASCII).Should().Be("text123");
            }
        }

        [Fact]
        public void Read_SecureAscii_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(2);

                Action action = () => reader.ReadSecureAscii();

                action.ShouldThrow<PacketReadException>();
            }
        }

        [Fact]
        public void Read_Length_For_SecureAscii_Exceeding_Data_Length()
        {
            using (var reader = new PacketReader(_data))
            {
                reader.SkipBytes(14);

                Action action = () => reader.ReadSecureAscii();

                action.ShouldThrow<PacketReadException>();
            }
        }

        [Fact]
        public void Read_Zero_Length_Should_Be_Null()
        {
            using (var reader = new PacketReader(new byte[] { 0, 0 }))
            {
                var result = reader.ReadSecureAscii();

                result.Should().BeNull();
            }
        }
    }
}