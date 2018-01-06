using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Kamina.Contracts.Common;

namespace Kamina.Common.Channel
{
    public static class ChannelAllowedExtensions
    {
        static ChannelAllowedExtensions()
        {
            AllowedChannels = new List<ulong> { ChannelId.DagcTextGames, ChannelId.DagcDankMemes, ChannelId.DagcGeneralChat };
        }

        public static Task<bool> AllowedToRespond(this ICommandContext context)
        {
            return Task.Run(async () =>
            {
                if (context.Guild?.Id == GuildId.Dagc)
                {
                    return await ChannelAllowed(context.Channel?.Id);
                }
                else
                {
                    return true;
                }
            });
        }

        public static Task<bool> IsTextGames(this ICommandContext context)
        {
            return Task.Run(() =>
            {
                if (context.Guild?.Id == GuildId.Dagc)
                {
                    return context.Channel?.Id == ChannelId.DagcTextGames;
                }
                return true;
            });
        }

        private static Task<bool> ChannelAllowed(ulong? channelId)
        {
            return Task.Run(() => channelId != null && AllowedChannels.Contains(channelId.Value));
        }

        private static readonly List<ulong> AllowedChannels;
    }
}
