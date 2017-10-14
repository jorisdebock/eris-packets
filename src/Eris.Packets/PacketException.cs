using System;

namespace Eris.Packets
{
    public class PacketReadException : Exception
    {
        public PacketReadException(string message)
            : base(message)
        {
        }
    }
}