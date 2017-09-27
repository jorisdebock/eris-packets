namespace Eris.Packets
{
    public class PacketReadAction
    {
        public long Count { get; }
        public string Message { get; }

        public PacketReadAction(long count, string message)
        {
            Count = count;
            Message = message;
        }
    }
}