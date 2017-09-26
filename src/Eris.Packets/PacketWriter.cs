using Eris.Extensions.Security;
using System;
using System.IO;
using System.Security;
using System.Text;

namespace Eris.Packets
{
    public class PacketWriter : IDisposable
    {
        private readonly MemoryStream _memoryStream;
        private readonly BinaryWriter _binaryWriter;

        public PacketWriter()
        {
            _memoryStream = new MemoryStream();
            _binaryWriter = new BinaryWriter(_memoryStream);
        }

        public byte[] GetBytes() => _memoryStream.ToArray();

        public void Flush() => _binaryWriter.Flush();

        public long Seek(int offset, SeekOrigin origin) => _binaryWriter.BaseStream.Seek(offset, origin);

        public void WriteUInt8Array(byte[] values) => WriteUInt8Array(values, 0, values.Length);

        public void WriteUInt8Array(byte[] values, int offset, int count) => _binaryWriter.Write(values, offset, count);

        public void WriteUInt8(byte value) => _binaryWriter.Write(value);

        public void WriteInt8(sbyte value) => _binaryWriter.Write(value);

        public void WriteUInt16(ushort value) => _binaryWriter.Write(value);

        public void WriteInt16(short value) => _binaryWriter.Write(value);

        public void WriteUInt32(uint value) => _binaryWriter.Write(value);

        public void WriteInt32(int value) => _binaryWriter.Write(value);

        public void WriteUInt64(ulong value) => _binaryWriter.Write(value);

        public void WriteInt64(long value) => _binaryWriter.Write(value);

        public void WriteBool(bool value) => _binaryWriter.Write(value);

        public void WriteFloat(float value) => _binaryWriter.Write(value);

        public void WriteAscii(string value)
        {
            var bytes = Encoding.ASCII.GetBytes(value);
            WriteUInt16((ushort)value.Length);
            WriteUInt8Array(bytes);
        }

        public void WriteSecureAscii(SecureString value)
        {
            var bytes = value.GetBytes(Encoding.ASCII);
            WriteUInt16((ushort)value.Length);
            WriteUInt8Array(bytes);
        }

        public void WriteUnicode(string value)
        {
            var bytes = Encoding.Unicode.GetBytes(value);
            WriteUInt16((ushort)(value.Length));
            WriteUInt8Array(bytes);
        }

        public void WriteSecureUnicode(SecureString value)
        {
            var bytes = value.GetBytes(Encoding.Unicode);
            WriteUInt16((ushort)value.Length);
            WriteUInt8Array(bytes);
        }

        public void Dispose()
        {
            _binaryWriter.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}