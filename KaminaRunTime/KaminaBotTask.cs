using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Audio;
using Kamina.Commands;
using Kamina.Commands.Logging;

namespace Kamina
{
    public sealed class KaminaBotTask : IBackgroundTask, IDisposable
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();

            taskInstance.Canceled += TaskInstance_Canceled;
           
            try
            {
                await Initialize();

            }
            catch (Exception ex)
            {
               await Logger.Log($"Exception on init: {ex} Stack: {ex.StackTrace} Inner: {ex.InnerException}");
                throw;
            }
            await Logger.Log("Done");
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            TryLogOut();
        }

        private async void TryLogOut()
        {
            await Logger.Log("Task Cancelled");
            if (client.LoginState == LoginState.LoggedIn)
            {
                client.LogoutAsync().RunSynchronously();
            }
        }

        private async Task Initialize()
        {
            await Logger.Log("Initializing");
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                AudioMode = AudioMode.Disabled,
                DefaultRetryMode = RetryMode.RetryRatelimit,
                MessageCacheSize = 5000,
            });
            
            string token = "<>";

#if DEBUG
            token = "<>;
#endif

            await InstallCommands();

            await client.LoginAsync(TokenType.Bot, token);
            await client.ConnectAsync();

            await Task.Delay(-1);
        }
        
        private async Task InstallCommands()
        {
            var map = new DependencyMap();
            map.Add(client);

            handler = new CommandHandler();
            await handler.Install(map);
        }

        private BackgroundTaskDeferral deferral;
        private DiscordSocketClient client;
        private DependencyMap map;
        private CommandHandler handler;

        public void Dispose()
        {
            TryLogOut();
            client?.Dispose();
        }
    }
}



