using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kamina.Common.Logging;
using Kamina.Contracts.Common;
using Kamina.Contracts.Logic;
using Kamina.Contracts.Objects;
using Kamina.Logic.Message;
using Kamina.Logic.WordResponse;

namespace Kamina.Logic
{
    public class CommandHandler
    {
        private CommandService commands;
        private DiscordShardedClient client;
        private IDependencyMap map;
        private IWordResponseLogic wordResponseLogic;
        private string help;

        public async Task Install(IDependencyMap _map)
        {
            try
            {
                // Create Command Service, inject it into Dependency Map
                client = _map.Get<DiscordShardedClient>();
                commands = new CommandService();
                _map.Add(commands);
                map = _map;
                client.MessageReceived += HandleCommand;
                client.Log += Client_Log;
                wordResponseLogic = new WordResponseLogic();
                await commands.AddModulesAsync(Assembly.Load(new AssemblyName("Kamina.Logic")));
            }
            catch (Exception ex)
            {
                await Logger.Log($"Exception: {ex}");
            }
        }

        public async Task HandleCommand(SocketMessage parameterMessage)
        {
            try
            {
                // Don't handle the command if it is a system message
                var message = parameterMessage as SocketUserMessage;
                if (message == null) return;

                char prefix = '>';

                // Mark where the prefix ends and the command begins
                var context = new CommandContext(client, message);
                if (client.CurrentUser != null)
                {
                    if (!message.Author.IsBot && message.Author.Id != client.CurrentUser.Id)
                    {
                      //  if (context.Guild != null)
                      //  {
                            await HandleMessageForGuild(context, message, prefix);
                     //   }
                        //else
                        //{
                        //    await HandleMessageForDm(context, message);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                await Logger.Log($"Exception: {ex}");
            }
        }

        private async Task HandleMessageForDm(CommandContext context, SocketUserMessage message)
        {
            await context.Channel.SendMessageAsync($"{context.User.Mention} I BELIEVE IN YOU TOOOOO");
        }

        private async Task HandleMessageForGuild(CommandContext context, SocketUserMessage message, char prefix)
        {
            // var defaultChanneld = context.Guild.DefaultChannelId;
            if (await AllowedToRespond(context)) return;

            int argPos = 0;
            var textResponse = await wordResponseLogic.HandleText(message.Content.ToLower());
            if (textResponse != null)
            {
                await HandleTextResponse(context, textResponse);
            }
            else
            {
                await HandleCommands(context, message, prefix, argPos);
            }

        }

        private async Task HandleCommands(CommandContext context, SocketUserMessage message, char prefix, int argPos)
        {
            // Determine if the message has a valid prefix, adjust argPos 
            if (
                !(message.HasMentionPrefix(client.CurrentUser, ref argPos) ||
                  message.HasCharPrefix(prefix, ref argPos))) return;

            // Create a Command Context
            // Execute the Command, store the result
            if (message.Content.Contains("I believe in you!"))
            {
                await context.Channel.SendMessageAsync($"{context.User.Mention} I BELIEVE IN YOU TOOOOO");
            }
            else
            {
                await commands.ExecuteAsync(context, argPos, map);

                // If the command failed, notify the user
#if debug
                        if (!result.IsSuccess)
                            await message.Channel.SendMessageAsync($"**Error:** {result.ErrorReason}");
#endif
            }
        }

        private async Task HandleTextResponse(CommandContext context, TextResponse textResponse)
        {
            string resultMsg = "";

            if (textResponse.ShouldMentionSender)
            {
                resultMsg = $"{context.User.Mention}: ";
            }

            resultMsg += $"{textResponse.Text}";
            var wordMsg = await context.Channel.SendMessageAsync(resultMsg);

            await AttemptToDeleteWordMessage(wordMsg.Id, wordMsg.Channel.Id);
        }

        private Task AttemptToDeleteWordMessage(ulong wordMsgId, ulong channelId)
        {
            return Task.Run(() =>
            {
               var deleteMessageTimer = new DeleteMessageTimer(wordMsgId, channelId, client);
            });
        }

        private Task<bool> AllowedToRespond(CommandContext context)
        {
            return Task.Run(() =>
            {
                //dankmemes id=283260580679385088
                if (context.Guild?.Id == GuildId.DAGC && context.Channel?.Id != ChannelId.DAGCDankMemes) //DAGC
                {
                    return true;
                }
                return false;
            });
        }

        private Task Client_Log(LogMessage arg)
        {
            try
            {
                return Logger.Log($"Log event: {arg.Message} Exception: {arg.Exception} Source: {arg.Source}");
            }
            catch (Exception)
            {
                /*nop*/
            }

            return Task.Delay(1);
        }
    }
}
