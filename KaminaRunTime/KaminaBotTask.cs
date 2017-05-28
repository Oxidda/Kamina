using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Audio;
using Kamina.Common;
using Kamina.Common.Logging;
using Kamina.Contracts;
using Kamina.Contracts.DataAccess;
using Kamina.Contracts.Logic;
using Kamina.DataAccess;
using Kamina.Logic;
using Kamina.Logic.Games;
using Kamina.Logic.Planning;
using Microsoft.EntityFrameworkCore;

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
                await client.LogoutAsync();
            }
        }

        private async Task Initialize()
        {
            await Logger.Log("Initializing");
            client = new DiscordShardedClient(new DiscordSocketConfig
            {
                AudioMode = AudioMode.Disabled,
                DefaultRetryMode = RetryMode.RetryRatelimit,
                MessageCacheSize = 10000,
                TotalShards = 2
            });

            string token;

            using (var reader = new FileReader().GetFileReader("token.txt"))
            {
                token = reader.ReadToEnd();
            }

            await InstallCommands();

            await client.LoginAsync(TokenType.Bot, token);
            await client.ConnectAsync();

            //var application = await client.GetApplicationInfoAsync();
            //client.
            

            await Task.Delay(-1);
        }
        
        private async Task InstallCommands()
        {
            //using (var dbContext = new KaminaDbContext())
            //{
            //    dbContext.Database.Migrate();
            //}
            

            map = new DependencyMap();
            map.Add<IWordsDataAccess>(new WordsDataAccess());
            map.Add<IHangmanState>(new HangmanState(map.Get<IWordsDataAccess>()));
            map.Add<IHangmanLogic>(new HangmanLogic(client , map.Get<IHangmanState>()));
            //map.Add<IKaminaDbContext>(new KaminaDbContext());
            //map.Add<IPlanningLogic>(new PlanningLogic(map.Get<IKaminaDbContext>()));
            map.Add(client);

            handler = new CommandHandler();
            await handler.Install(map);
        }

        private BackgroundTaskDeferral deferral;
        private DiscordShardedClient client;
        private DependencyMap map;
        private CommandHandler handler;

        public void Dispose()
        {
            TryLogOut();
            client?.Dispose();
        }
    }
}



