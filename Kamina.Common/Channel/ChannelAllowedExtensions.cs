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
            AllowedChannels = new List<ulong> { ChannelId.DagcTextGames, ChannelId.DagcDankMemes, ChannelId.DagcGeneralChat, ChannelId.DagcVoiceChat };
        }

        public static Task<bool> AllowedToRespond(this ICommandContext context)
        {
            return Task.Run(async () =>
            {
                if (context.IsInDagc())
                {
                    return await ChannelAllowed(context.Channel?.Id);
                }
                return true;
            });
        }

        public static bool IsInDagc(this ICommandContext context)
        {
            return context.Guild?.Id == GuildId.Dagc;
        }

        public static Task<bool> IsTextGames(this ICommandContext context)
        {
            return Task.Run(() =>
            {
                if (context.IsInDagc())
                {
                    return context.Channel?.Id == ChannelId.DagcTextGames;
                }
                return true;
            });
        }

        public static Task<bool> IsVoice(this ICommandContext context)
        {
            return Task.Run(() =>
            {
                if (context.IsInDagc())
                {
                    return context.Channel?.Id == ChannelId.DagcVoiceChat;
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
