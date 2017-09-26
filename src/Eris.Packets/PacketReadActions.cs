using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Eris.Packets
{
    public class PacketReadActions
    {
        public IReadOnlyList<PacketReadAction> Actions { get; }

        public byte[] Data { get; }

        public PacketReadActions(byte[] data, IList<PacketReadAction> packetReadActions)
        {
            Data = data;
            Actions = new ReadOnlyCollection<PacketReadAction>(packetReadActions);
        }
    }
}