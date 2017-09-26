namespace Eris.Packets
{
    public class PacketReadAction
    {
        public long ReadCount { get; }
        public string Message { get; }

        public PacketReadAction(long readCount, string message)
        {
            ReadCount = readCount;
            Message = message;
        }
    }
}