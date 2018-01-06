using System;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using Kamina.Common.Comparer;
using Kamina.Common.Logging;
using Kamina.Contracts.Common;

namespace Kamina.Logic.Commands
{
    public sealed class CommonCommand : ModuleBase
    {
        public CommonCommand(CommandService service, DiscordShardedClient client)
        {
            _service = service;
            _client = client;
        }

        [Command("info")]

        public async Task Info()
        {
            try
            {
                var time = DateTime.Now - Logger.StartTime;

                var application = await Context.Client.GetApplicationInfoAsync();
                var discordSocketClient = Context.Client as DiscordShardedClient;
                if (discordSocketClient != null)
                    await ReplyAsync(
                        $"{Format.Bold("Info")}\n" +
                        $"- Author: {application.Owner.Username} (ID {application.Owner.Id})\n" +
                        $"- Library: Discord.Net ({DiscordConfig.Version})\n" +
                        $"- Runtime: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture}\n" +
                        $"- Uptime: {time}\n\n" +
                        $"{Format.Bold("Stats")}\n" +
                        $"- Heap Size: {GetHeapSize()} MB\n" +
                        $"- Guilds: {discordSocketClient.Guilds.Count}\n" +
                        $"- Channels: {discordSocketClient.Guilds.Sum(g => g.Channels.Count)}\n" +
                        $"- Users: {discordSocketClient.Guilds.Sum(g => g.Users.Count)}"
                    );
            }
            catch (Exception ex)
            {
                await Logger.LogAsync($"Error getting info : {ex}");
            }
        }


        [Command("help")]
        public async Task Help()
        {
            string prefix = ">";
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "These are the commands you can use"
            };

            foreach (var module in _service.Modules)
            {
                string description = null;
                foreach (var cmd in module.Commands.Distinct(new CommandInfoComparer()).ToList())
                {
                    var result = await cmd.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                    {
                        description += $"{prefix}{cmd.Aliases.First()}\n";
                    }
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    builder.AddField(x =>
                    {
                        x.Name = module.Name;
                        x.Value = description;
                        x.IsInline = false;
                    });
                }
            }

            await ReplyAsync("", false, builder.Build());
        }

        [Command("Say")]
        public async Task Say(string message)
        {
            try
            {
                var application = await Context.Client.GetApplicationInfoAsync();

                if (Context.Message.Author.Id == application.Owner.Id)
                {
                    await SayWithGuild(message, 0);
                }

            }
            catch (Exception ex)
            {
                await Logger.LogAsync($"Error with command say : {ex}");
            }
        }

        [Command("Say")]
        public async Task SayWithGuild(string message, ulong guildName)
        {
            try
            {
                var application = await Context.Client.GetApplicationInfoAsync();

                if (Context.Message.Author.Id == application.Owner.Id)
                {
                    if (guildName == 0)
                    {
                        //string message = Context.Message.Content;
                        //message = message.Remove(0, 2);
                        await ReplyAsync(message);
                    }
                    else
                    {
                        //199851384944852992 && context.Channel?.Id != 283260580679385088
                        var guild = _client.GetGuild(GuildId.Dagc);
                        var channel = guild?.Channels.FirstOrDefault(x => x.Id == ChannelId.DagcDankMemes) as SocketTextChannel;
                        if (channel != null)
                        {
                            await channel.SendMessageAsync(message);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                await Logger.LogAsync($"Error with command SayWithGuild : {ex}");
            }
        }

        private static string GetHeapSize()
            => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString(CultureInfo.InvariantCulture);
        private CommandService _service;
        private readonly DiscordShardedClient _client;
    }
}


