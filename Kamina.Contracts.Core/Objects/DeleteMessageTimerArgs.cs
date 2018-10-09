namespace Kamina.Contracts.Core.Objects
{
   public class DeleteMessageTimerArgs
    {
        public ulong MessageId { get; set; }
        public ulong ChannelId { get; set; }
    }
}
