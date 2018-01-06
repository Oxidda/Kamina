using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kamina.Common.Channel;
using Kamina.Common.Logging;
using Kamina.Contracts.Common;
using Kamina.Contracts.Logic;
using Kamina.Contracts.Objects;
using Kamina.Logic.Message;
using Kamina.Logic.WordResponse;
using Microsoft.Extensions.DependencyInjection;

namespace Kamina.Logic
{
    public class CommandHandler
    {
        public CommandHandler(IServiceProvider serviceProvider)
        {
            try
            {
                _commands = new CommandService();
                _serviceProvider = serviceProvider;
                _client = serviceProvider.GetService<DiscordShardedClient>();
                _timerQueue = new TimerQueue();
        
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
                _wordResponseLogic = new WordResponseLogic();
                _client.MessageReceived += HandleCommand;
                _client.Log += Client_Log;

                await _commands.AddModulesAsync(Assembly.Load(new AssemblyName("Kamina.Logic")));
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
                var message = parameterMessage as SocketUserMessage;
                if (message == null) return;

                var prefix = '>';

                // Mark where the prefix ends and the command begins
                var context = new CommandContext(_client, message);
                if (_client.CurrentUser != null)
                    if (!message.Author.IsBot && message.Author.Id != _client.CurrentUser.Id)
                        await HandleMessageForGuild(context, message, prefix);
            }
            catch (Exception ex)
            {
                await Logger.LogAsync($"Exception: {ex}");
            }
        }

        //private async Task HandleMessageForDm(CommandContext context, SocketUserMessage message)
        //{
        //    await context.Channel.SendMessageAsync($"{context.User.Mention} I BELIEVE IN YOU TOOOOO");
        //}

        private async Task HandleMessageForGuild(CommandContext context, SocketUserMessage message, char prefix)
        {
            bool allowedToRespond = await context.AllowedToRespond();
            if (!allowedToRespond) return;

            var argPos = 0;
            var textResponse = await _wordResponseLogic.HandleText(message.Content.ToLower());
            if (textResponse != null)
                await HandleTextResponse(context, textResponse);
            else
                await HandleCommands(context, message, prefix, argPos);
        }

        private async Task HandleCommands(CommandContext context, SocketUserMessage message, char prefix, int argPos)
        {
            // Determine if the message has a valid prefix, adjust argPos 
            if (
                !(message.HasMentionPrefix(_client.CurrentUser, ref argPos) ||
                  message.HasCharPrefix(prefix, ref argPos))) return;

            // Create a Command Context
            // Execute the Command, store the result
            if (message.Content.Contains("I believe in you!"))
            {
                await context.Channel.SendMessageAsync($"{context.User.Mention} I BELIEVE IN YOU TOOOOO");
            }
            else
            {
                await _commands.ExecuteAsync(context, argPos, _serviceProvider);
                // If the command failed, notify the user
#if debug
                        if (!result.IsSuccess)
                            await message.Channel.SendMessageAsync($"**Error:** {result.ErrorReason}");
#endif
            }
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
                _timerQueue.QueueNewDeleteTimer(wordMsgId, channelId, _client);
            });
        }

        private Task Client_Log(LogMessage arg)
        {
            try
            {
                return Logger.LogAsync($"Log event: {arg.Message} Exception: {arg.Exception} Source: {arg.Source}");
            }
            catch (Exception)
            {
                /*nop*/
            }

            return Task.Delay(1);
        }

     
        private readonly DiscordShardedClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _serviceProvider;
        private IWordResponseLogic _wordResponseLogic;
        private readonly TimerQueue _timerQueue;

    }
}