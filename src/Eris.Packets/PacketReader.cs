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
        private readonly byte[] _bytes;
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
                _bytes = bytes;
                _packetReadActions = new List<PacketReadAction>();
            }
        }

        public PacketReadActions GetPacketReadActions()
        {
            if (_bytes == null || _packetReadActions == null)
            {
                return null;
            }

            return new PacketReadActions(_bytes, _packetReadActions);
        }

        public bool HasMore() => _binaryReader.BaseStream.Length > _binaryReader.BaseStream.Position;

        public void SkipBytes(long length, string message = null)
        {
            AddReadAction(length, $"Skip ({length}): {message}");
            _binaryReader.BaseStream.Position += length;
        }

        public byte[] ReadRemainingBytes(string message = null)
        {
            var length = _binaryReader.BaseStream.Length - _binaryReader.BaseStream.Position;
            AddReadAction(length, $"Remaining ({length}): {message}");

            try
            {
                return _binaryReader.ReadBytes((int)length);
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
            }
        }

        public byte[] ReadUInt8Array(int length, string message = null)
        {
            AddReadAction(length, $"UInt8Array ({length}): {message}");

            try
            {
                return _binaryReader.ReadBytes(length);
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
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
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
            }
            catch (InvalidCastException e)
            {
                throw new PacketReadException($"{e.Message}: {message}, make sure the enum inherited from byte!", GetPacketReadActions());
            }

            AddReadAction(1, $"Enum ({enumValue}): {message}");

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
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
            }
            catch (InvalidCastException e)
            {
                throw new PacketReadException($"{e.Message}: {message}, make sure the enum inherited from ushort!", GetPacketReadActions());
            }

            AddReadAction(1, $"Enum ({enumValue}): {message}");

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
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
            }
            catch (InvalidCastException e)
            {
                throw new PacketReadException($"{e.Message}: {message}, make sure the enum inherited from uint!", GetPacketReadActions());
            }

            AddReadAction(1, $"Enum ({enumValue}): {message}");

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
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
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
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
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
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
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
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
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
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
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
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
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
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
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
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
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
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
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
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
            }

            AddReadAction(4, $"Float ({result}): {message}");

            return result;
        }

        public string ReadAscii(string message = null)
        {
            AddReadAction(2, $"Ascii length: {message}");

            try
            {
                var length = _binaryReader.ReadUInt16();
                return length > 0 ? ReadAscii(length, message) : null;
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
            }
        }

        public string ReadAscii(int length, string message = null)
        {
            byte[] bytes;

            try
            {
                bytes = _binaryReader.ReadBytes(length);
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
            }

            var text = Encoding.ASCII.GetString(bytes);
            AddReadAction(length, $"Ascii ({length}) - \"{text}\": {message}");
            return text;
        }

        public SecureString ReadSecureAscii(string message = null)
        {
            AddReadAction(2, $"Ascii length: {message}");

            try
            {
                var length = _binaryReader.ReadUInt16();
                return length > 0 ? ReadSecureAscii(length, message) : null;
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
            }
        }

        public SecureString ReadSecureAscii(int length, string message = null)
        {
            byte[] bytes;

            try
            {
                bytes = _binaryReader.ReadBytes(length);
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
            }

            var secureString = new SecureString();
            foreach (var c in Encoding.ASCII.GetChars(bytes))
            {
                secureString.AppendChar(c);
            }
            AddReadAction(length, $"Ascii ({length}) - \"********\": {message}");
            return secureString;
        }

        public string ReadUnicode(string message = null)
        {
            AddReadAction(2, $"Unicode length: {message}");

            try
            {
                var length = _binaryReader.ReadUInt16() * 2;
                return length > 0 ? ReadUnicode(length) : null;
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
            }
        }

        public string ReadUnicode(int length, string message = null)
        {
            byte[] bytes;

            try
            {
                bytes = _binaryReader.ReadBytes(length);
            }
            catch (EndOfStreamException e)
            {
                throw new PacketReadException($"{e.Message}: {message}", GetPacketReadActions());
            }

            var text = Encoding.Unicode.GetString(bytes);
            AddReadAction(length, $"Unicode ({length}) - \"{text}\": {message}");

            return text;
        }

        public void Dispose()
        {
            _binaryReader.Dispose();
            GC.SuppressFinalize(this);
        }

        private void AddReadAction(long readCount, string message) => _packetReadActions?.Add(new PacketReadAction(readCount, message));
    }
}