using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Kamina.Contracts.Objects;

namespace Kamina.Logic.Message
{
    public class DeleteMessageTimer : IDisposable
    {
        private readonly IDiscordClient _client;
        private readonly Timer _timer;
        public DeleteMessageTimer(ulong messageId, ulong channelId, IDiscordClient client)
        {
            _client = client;
            _timer = new Timer(OnTimedEvent, new DeleteMessageTimerArgs
            {
                ChannelId = channelId,
                MessageId = messageId,
            }, TimeSpan.FromSeconds(30), new TimeSpan(-1));
        }

        private void OnTimedEvent(object state)
        {
            var deleteArgs = (state as DeleteMessageTimerArgs);
            if (deleteArgs != null)
            {
                Task.Run(async () =>
                {
                    var textChannel = await GetChannel(deleteArgs);
                    if (textChannel != null)
                    {
                        await DeleteMessage(deleteArgs, textChannel);
                    }
                });
            }
        }

        private async Task<ITextChannel> GetChannel(DeleteMessageTimerArgs deleteArgs)
        {
            var channel = await _client.GetChannelAsync(deleteArgs.ChannelId);
            var textChannel = channel as ITextChannel;
            return textChannel;
        }

        private static async Task DeleteMessage(DeleteMessageTimerArgs deleteArgs, ITextChannel textChannel)
        {
            var message = await textChannel.GetMessageAsync(deleteArgs.MessageId);
            if (message != null)
            {
                await textChannel.DeleteMessagesAsync(new[] { message });
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
            _timer?.Dispose();
        }
    }
}
