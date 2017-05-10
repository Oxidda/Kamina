using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kamina.Common.Logging;
using Kamina.Contracts.Logic;
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
                int argPos = 0;
                var textResponse = await wordResponseLogic.HandleText(message.Content.ToLower());
                if (textResponse != null && !message.Author.IsBot && message.Author.Id != client.CurrentUser.Id)
                {
                        var context = new CommandContext(client, message);

                        string resultMsg = "";

                        if (textResponse.ShouldMentionSender)
                        {
                            resultMsg = $"{context.User.Mention}: ";
                        }

                        resultMsg += $"{textResponse.Text}";
                        await context.Channel.SendMessageAsync(resultMsg);
                }
                else
                {
                    var context = new CommandContext(client, message);
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
                        var result = await commands.ExecuteAsync(context, argPos, map);

                        // If the command failed, notify the user
                        if (!result.IsSuccess)
                            await message.Channel.SendMessageAsync($"**Error:** {result.ErrorReason}");
                    }
                }
            }
            catch (Exception ex)
            {
                await Logger.Log($"Exception: {ex}");
            }
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
