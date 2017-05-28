namespace Kamina.Contracts.Objects
{
   public class DeleteMessageTimerArgs
    {
        public ulong MessageId { get; set; }
        public ulong ChannelId { get; set; }
    }
}
