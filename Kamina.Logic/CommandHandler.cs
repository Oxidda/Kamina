using Discord.Commands;
using Discord.WebSocket;
using Kamina.Common.Channel;
using Kamina.Common.Logging;
using Kamina.Contracts.Logic;
using Kamina.Contracts.Objects;
using Kamina.Logic.Message;
using Kamina.Logic.WordResponse;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Kamina.Contracts.Common;

namespace Kamina.Logic
{
    public class CommandHandler
    {
        private readonly DiscordShardedClient client;
        private readonly CommandService commands;
        private readonly IServiceProvider serviceProvider;
        private IWordResponseLogic wordResponseLogic;
        private readonly TimerQueue timerQueue;

        public CommandHandler(IServiceProvider serviceProvider)
        {
            try
            {
                commands = new CommandService();
                this.serviceProvider = serviceProvider;
                client = serviceProvider.GetService<DiscordShardedClient>();
                timerQueue = new TimerQueue();

            }
            catch (Exception ex)
            {
                Logger.LogAsync($"Exception: {ex}").GetAwaiter().GetResult();
            }
        }

        public async Task Start()
        {
            try
            {
                wordResponseLogic = new WordResponseLogic();
                client.MessageReceived += HandleCommand;
                //client.Log += Client_Log;

                client.UserJoined += Client_UserJoined;

                await commands.AddModulesAsync(Assembly.Load(new AssemblyName("Kamina.Logic")));

            }
            catch (Exception ex)
            {
                Logger.LogAsync($"Exception: {ex}").GetAwaiter().GetResult();
            }
        }
        
        public async Task HandleCommand(SocketMessage parameterMessage)
        {
            try
            {
                // Don't handle the command if it is a system message
                if (!(parameterMessage is SocketUserMessage message)) return;

                char prefix = '>';

#if DEBUG
                prefix = '!';
#endif
                // Mark where the prefix ends and the command begins
                var context = new CommandContext(client, message);
                if (client.CurrentUser != null)
                    if (!message.Author.IsBot && message.Author.Id != client.CurrentUser.Id)
                        await HandleMessageForGuild(context, message, prefix);
            }
            catch (Exception ex)
            {
                await Logger.LogAsync($"Exception: {ex}");
            }
        }

        private async Task HandleMessageForGuild(CommandContext context, SocketUserMessage message, char prefix)
        {
            bool allowedToRespond = await context.AllowedToRespond();
            if (!allowedToRespond) return;

            var argPos = 0;
            if (!HasPrefix(message, prefix, ref argPos))
            {
                var textResponse = await wordResponseLogic.HandleText(message.Content.ToLower());
                if (textResponse != null)
                    await HandleTextResponse(context, textResponse);
            }
            else
            {
                await HandleCommands(context, message, argPos);
            }
        }

        private async Task Client_UserJoined(SocketGuildUser arg)
        {
            if (arg.Guild.Id == GuildId.Dagc)
            {
                if (client.GetChannel(ChannelId.DagcGeneralChat) is SocketTextChannel channel)
                {
                    await channel.SendMessageAsync($"Welkom {arg.Mention}!");
                }
            }
        }

        private async Task HandleCommands(CommandContext context, SocketUserMessage message, int argPos)
        {
            if (message.Content.Contains("I believe in you!"))
            {
                await context.Channel.SendMessageAsync($"{context.User.Mention} I BELIEVE IN YOU TOOOOO");
            }
            else
            {
                await commands.ExecuteAsync(context, argPos, serviceProvider);
#if debug
                        if (!result.IsSuccess)
                            await message.Channel.SendMessageAsync($"**Error:** {result.ErrorReason}");
#endif
            }
        }

        private bool HasPrefix(SocketUserMessage message, char prefix, ref int argPos)
        {
            return (message.HasMentionPrefix(client.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos));
        }

        private async Task HandleTextResponse(CommandContext context, TextResponse textResponse)
        {
            var resultMsg = "";

            if (textResponse.ShouldMentionSender)
                resultMsg = $"{context.User.Mention}: ";

            resultMsg += $"{textResponse.Text}";
            var wordMsg = await context.Channel.SendMessageAsync(resultMsg);

            await AttemptToDeleteWordMessage(wordMsg.Id, wordMsg.Channel.Id);
        }

        private Task AttemptToDeleteWordMessage(ulong wordMsgId, ulong channelId)
        {
            return Task.Run(() =>
            {
                timerQueue.QueueNewDeleteTimer(wordMsgId, channelId, client);
            });
        }

        //private Task Client_Log(LogMessage arg)
        //{
        //    try
        //    {
        //        return Logger.LogAsync($"Log event: {arg.Message} Exception: {arg.Exception} Source: {arg.Source}");
        //    }
        //    catch (Exception)
        //    {
        //        /*nop*/
        //    }

        //    return Task.Delay(1);
        //}
    }
}