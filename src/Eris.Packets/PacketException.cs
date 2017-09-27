using System;
using System.Collections.Generic;

namespace Eris.Packets
{
    public class PacketReadException : Exception
    {
        public IReadOnlyList<PacketReadAction> PacketReadActions { get; }

        public PacketReadException(string message, IReadOnlyList<PacketReadAction> packetReadActions = null)
            : base(message)
        {
            PacketReadActions = packetReadActions;
        }
    }
}