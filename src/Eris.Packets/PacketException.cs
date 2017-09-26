using System;

namespace Eris.Packets
{
    public class PacketReadException : Exception
    {
        public PacketReadActions PacketReadActions { get; }

        public PacketReadException(string message, PacketReadActions packetReadActions = null) : base(message) => PacketReadActions = packetReadActions;
    }
}