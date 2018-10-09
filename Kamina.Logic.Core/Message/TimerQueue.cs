using System.Collections.Generic;
using Discord;

namespace Kamina.Logic.Core.Message
{
   public class TimerQueue
    {
        public TimerQueue()
        {
            _timers = new List<BaseTimer>();
        }

        public void QueueNewDeleteTimer(ulong messageId, ulong channelId, IDiscordClient client)
        {
            DeleteMessageTimer timer = new DeleteMessageTimer(messageId, channelId, client,TimerEnded);
            _timers.Add(timer);
        }

        private void TimerEnded(BaseTimer timer)
        {
            _timers.Remove(timer);
        }

        private readonly List<BaseTimer> _timers;
    }
}
