using Eris.Extensions.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;

namespace Eris.Packets
{
    public class PacketReader : IDisposable
    {
        private readonly BinaryReader _binaryReader;
        private readonly List<PacketReadAction> _packetReadActions;

        public PacketReader(byte[] bytes, bool collectReadActions = false)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            _binaryReader = new BinaryReader(new MemoryStream(bytes, writable: false));

            if (collectReadActions)
            {
                _packetReadActions = new List<PacketReadAction>();
            }
        }

        public IReadOnlyList<PacketReadAction> GetPacketReadActions() => _packetReadActions?.AsReadOnly();

        public bool HasMore() => _binaryReader.BaseStream.Length > _binaryReader.BaseStream.Position;

        public void SkipBytes(long length, string message = null)
        {
            AddReadAction(length, $"Skip ({length}): {message}");

            try
            {
                ThrowIfCanNotRead(length);
                _binaryReader.BaseStream.Position += length;
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public byte[] ReadRemainingBytes(string message = null)
        {
            var length = _binaryReader.BaseStream.Length - _binaryReader.BaseStream.Position;
            AddReadAction(length, $"Remaining ({length}): {message}");

            return ReadBytes((int)length);
        }

        public byte[] ReadUInt8Array(int length, string message = null)
        {
            AddReadAction(length, $"UInt8Array ({length}): {message}");

            try
            {
                return ReadBytes(length);
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public T ReadUInt8<T>(string message = null) where T : struct, IConvertible
        {
            T enumValue;
            try
            {
                enumValue = (T)(object)_binaryReader.ReadByte();
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
            catch (InvalidCastException e)
            {
                throw new PacketReadException($"{e.Message}: {message}, make sure the enum inherited from byte!");
            }

            AddReadAction(1, $"Enum8 ({enumValue}): {message}");

            return enumValue;
        }

        public T ReadUInt16<T>(string message = null) where T : struct, IConvertible
        {
            T enumValue;
            try
            {
                enumValue = (T)(object)_binaryReader.ReadUInt16();
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
            catch (InvalidCastException e)
            {
                throw new PacketReadException($"{e.Message}: {message}, make sure the enum inherited from ushort!");
            }

            AddReadAction(2, $"Enum16 ({enumValue}): {message}");

            return enumValue;
        }

        public T ReadUInt32<T>(string message = null) where T : struct, IConvertible
        {
            T enumValue;
            try
            {
                enumValue = (T)(object)_binaryReader.ReadUInt32();
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
            catch (InvalidCastException e)
            {
                throw new PacketReadException($"{e.Message}: {message}, make sure the enum inherited from uint!");
            }

            AddReadAction(4, $"Enum32 ({enumValue}): {message}");

            return enumValue;
        }

        public T ReadUInt64<T>(string message = null) where T : struct, IConvertible
        {
            T enumValue;
            try
            {
                enumValue = (T)(object)_binaryReader.ReadUInt64();
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
            catch (InvalidCastException e)
            {
                throw new PacketReadException($"{e.Message}: {message}, make sure the enum inherited from ulong!");
            }

            AddReadAction(8, $"Enum64 ({enumValue}): {message}");

            return enumValue;
        }

        public byte ReadUInt8(string message = null)
        {
            AddReadAction(1, $"UInt8: {message}");

            try
            {
                return _binaryReader.ReadByte();
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public sbyte ReadInt8(string message = null)
        {
            AddReadAction(1, $"Int8: {message}");

            try
            {
                return _binaryReader.ReadSByte();
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public ushort ReadUInt16(string message = null)
        {
            AddReadAction(2, $"UInt16: {message}");

            try
            {
                return _binaryReader.ReadUInt16();
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public short ReadInt16(string message = null)
        {
            AddReadAction(2, $"Int16: {message}");

            try
            {
                return _binaryReader.ReadInt16();
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public uint ReadUInt32(string message = null)
        {
            AddReadAction(4, $"UInt32: {message}");

            try
            {
                return _binaryReader.ReadUInt32();
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public int ReadInt32(string message = null)
        {
            AddReadAction(4, $"Int32: {message}");

            try
            {
                return _binaryReader.ReadInt32();
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public ulong ReadUInt64(string message = null)
        {
            AddReadAction(8, $"UInt64: {message}");

            try
            {
                return _binaryReader.ReadUInt64();
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public long ReadInt64(string message = null)
        {
            AddReadAction(8, $"Int64: {message}");

            try
            {
                return _binaryReader.ReadInt64();
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public bool ReadBool(string message = null)
        {
            var result = false;
            try
            {
                result = _binaryReader.ReadByte() == 1;
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }

            AddReadAction(1, $"Bool ({result}): {message}");

            return result;
        }

        public float ReadFloat(string message = null)
        {
            float result;
            try
            {
                result = _binaryReader.ReadSingle();
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }

            AddReadAction(4, $"Float ({result}): {message}");

            return result;
        }

        public string ReadAscii(string message = null)
        {
            try
            {
                var length = _binaryReader.ReadUInt16();

                AddReadAction(2, $"Ascii length ({length}): {message}");

                return length > 0 ? ReadAscii(length, message) : null;
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public string ReadAscii(int length, string message = null)
        {
            byte[] bytes;

            try
            {
                bytes = ReadBytes(length);
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }

            var text = Encoding.ASCII.GetString(bytes);
            AddReadAction(length, $"Ascii (\"{text}\"): {message}");
            return text;
        }

        public SecureString ReadSecureAscii(string message = null)
        {
            try
            {
                var length = _binaryReader.ReadUInt16();

                AddReadAction(2, $"Ascii length ({length}): {message}");

                return length > 0 ? ReadSecureAscii(length, message) : null;
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public SecureString ReadSecureAscii(int length, string message = null)
        {
            byte[] bytes;

            try
            {
                bytes = ReadBytes(length);
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }

            var secureString = bytes.ToSecureString(Encoding.ASCII);

            AddReadAction(length, $"Ascii (\"********\"): {message}");
            return secureString;
        }

        public string ReadUnicode(string message = null)
        {
            try
            {
                var length = _binaryReader.ReadUInt16() * 2;

                AddReadAction(2, $"Unicode length ({length}): {message}");

                return length > 0 ? ReadUnicode(length, message) : null;
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public string ReadUnicode(int length, string message = null)
        {
            byte[] bytes;

            try
            {
                bytes = ReadBytes(length);
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }

            var text = Encoding.Unicode.GetString(bytes);
            AddReadAction(length, $"Unicode (\"{text}\"): {message}");

            return text;
        }

        public SecureString ReadSecureUnicode(string message = null)
        {
            try
            {
                var length = _binaryReader.ReadUInt16() * 2;

                AddReadAction(2, $"Unicode length ({length}): {message}");

                return length > 0 ? ReadSecureUnicode(length, message) : null;
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }
        }

        public SecureString ReadSecureUnicode(int length, string message = null)
        {
            byte[] bytes;

            try
            {
                bytes = ReadBytes(length);
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}");
            }

            var secureString = bytes.ToSecureString(Encoding.Unicode);

            AddReadAction(length, $"Unicode (\"********\"): {message}");
            return secureString;
        }

        public void Dispose()
        {
            _binaryReader.Dispose();
            GC.SuppressFinalize(this);
        }

        private void AddReadAction(long count, string message) => _packetReadActions?.Add(new PacketReadAction(count, message));

        private byte[] ReadBytes(int length)
        {
            ThrowIfCanNotRead(length);

            return _binaryReader.ReadBytes(length);
        }

        private void ThrowIfCanNotRead(long length)
        {
            if (_binaryReader.BaseStream.Position + length > _binaryReader.BaseStream.Length)
            {
                throw new EndOfStreamException("End of stream reached");
            }
        }
    }
}