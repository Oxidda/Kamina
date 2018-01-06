using Discord;
using Discord.WebSocket;
using Kamina.Common;
using Kamina.Common.Logging;
using Kamina.Contracts.Logic;
using Kamina.DataAccess;
using Kamina.Logic;
using Kamina.Logic.Games;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace Kamina
{
    public sealed class KaminaBotTask : IBackgroundTask, IDisposable
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();

            taskInstance.Canceled += TaskInstance_Canceled;
           
            try
            {
                await Initialize();
            }
            catch (Exception ex)
            {
               await Logger.LogAsync($"Exception on init: {ex} Stack: {ex.StackTrace} Inner: {ex.InnerException}");
                throw;
            }
            await Logger.LogAsync("Done");
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            TryLogOut();
        }

        private async void TryLogOut()
        {
            await Logger.LogAsync("Task Cancelled");
            if (_client.LoginState == LoginState.LoggedIn)
            {
                await _client.LogoutAsync();
            }
        }

        private async Task Initialize()
        {
            await Logger.LogAsync("Initializing");
            _client = new DiscordShardedClient(new DiscordSocketConfig
            {
                DefaultRetryMode = RetryMode.RetryRatelimit,
                MessageCacheSize = 10000,
                TotalShards = 2
            });

            string token;
            using (var reader = new FileReader().GetFileReader("token.txt"))
            {
                token = reader.ReadToEnd();
            }
            IServiceProvider services = InstallCommands();
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            CommandHandler manager = services.GetService<CommandHandler>();
            await manager.Start();
            await Task.Delay(-1);
        }
        
        private IServiceProvider InstallCommands()
        {
            _map = new ServiceCollection();
            _map.AddSingleton<IWordsDataAccess, WordsDataAccess>();
            _map.AddSingleton<IHangmanState, HangmanState>();
            _map.AddSingleton<IHangmanLogic, HangmanLogic>();
            _map.AddSingleton(_client);
            _map.AddSingleton<CommandHandler>();

            var provider = new DefaultServiceProviderFactory().CreateServiceProvider(_map);
            return provider;
        }

        private BackgroundTaskDeferral _deferral;
        private DiscordShardedClient _client;
        private IServiceCollection _map;

        public void Dispose()
        {
            TryLogOut();
            _client?.Dispose();
        }
    }
}



