using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kamina.Commands.Logging;


namespace Kamina.Commands
{
    public class CommandHandler
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private IDependencyMap map;
        private string help;
        public async Task Install(IDependencyMap _map)
        {
            try
            {
                // Create Command Service, inject it into Dependency Map
                client = _map.Get<DiscordSocketClient>();
                commands = new CommandService();
                _map.Add(commands);
                map = _map;
                client.MessageReceived += HandleCommand;
                client.Log += Client_Log;

                //                client.Disconnected += Client_Disconnected;

                await commands.AddModulesAsync(Assembly.Load(new AssemblyName("Kamina.Commands")));

                help = "help: ";
                foreach (var c in commands.Commands)
                {
                    help += $"{c.Name}, ";
                }

            }
            catch (Exception ex)
            {
                await Logger.Log($"Exception: {ex}");
            }
        }

        //private Task Client_Disconnected(Exception arg)
        //{
        //  if(client)

        //}

        public async Task HandleCommand(SocketMessage parameterMessage)
        {
            try
            {
                // Don't handle the command if it is a system message
                var message = parameterMessage as SocketUserMessage;
                if (message == null) return;


                char prefix = '>';
#if DEBUG
                prefix = '|';
#endif


                // Mark where the prefix ends and the command begins
                int argPos = 0;
                // Determine if the message has a valid prefix, adjust argPos 
                if (!(message.HasMentionPrefix(client.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos))) return;

                // Create a Command Context
                var context = new CommandContext(client, message);
                // Execute the Command, store the result

                //if (context.Message.Content.EndsWith("help"))
                //{
                //    await context.Channel.SendMessageAsync(help);
                //}
                //else
                //{
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
                //}
            }
            catch (Exception ex)
            {
                await Logger.Log($"Exception: {ex}");
            }
        }

        private bool HasHeyPrefix(SocketUserMessage message, int argPos)
        {
            return message.HasCharPrefix('>', ref argPos);
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
