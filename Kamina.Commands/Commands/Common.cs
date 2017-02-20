using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kamina.Common.Logging;

namespace Kamina.Logic.Commands
{
    public class CommonCommand : ModuleBase
    {
        public CommonCommand(CommandService service)
        {
            this.service = service;
        }

        [Command("info")]
        
        public async Task Info()
        {
            try
            {
                var time = DateTime.Now - Logger.StartTime;

                var application = await Context.Client.GetApplicationInfoAsync();
                var discordSocketClient = Context.Client as DiscordSocketClient;
                if (discordSocketClient != null)
                    await ReplyAsync(
                        $"{Format.Bold("Info")}\n" +
                        $"- Author: {application.Owner.Username} (ID {application.Owner.Id})\n" +
                        $"- Library: Discord.Net ({DiscordConfig.Version})\n" +
                        $"- Runtime: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture}\n" +
                        $"-Uptime: {time}\n\n" +
                        $"{Format.Bold("Stats")}\n" +
                        $"- Heap Size: {GetHeapSize()} MB\n" +
                        $"- Guilds: {discordSocketClient.Guilds.Count}\n" +
                        $"- Channels: {discordSocketClient.Guilds.Sum(g => g.Channels.Count)}\n" +
                        $"- Users: {discordSocketClient.Guilds.Sum(g => g.Users.Count)}"
                    );
            }
            catch (Exception ex)
            {

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

            foreach (var module in service.Modules)
            {
                string description = null;
                foreach (var cmd in module.Commands)
                {
                    var result = await cmd.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                        description += $"{prefix}{cmd.Aliases.First()}\n";
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
                    //string message = Context.Message.Content;

                    //message = message.Remove(0, 2);

                    await ReplyAsync(message);
                }

            }
            catch (Exception ex)
            {

            }
        }

        private static string GetHeapSize()
            => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString(CultureInfo.InvariantCulture);
        private CommandService service;
    }
}


