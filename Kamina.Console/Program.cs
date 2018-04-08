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
using Kamina.Logic.Audio;

namespace Kamina.Console
{
    class Program
    {
        private static DiscordShardedClient client;
        private static IServiceCollection map;
        static void Main(string[] args)
        {
            try
            {
                Task.WaitAll(Run());
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
            System.Console.ReadLine();
        }

        private static async Task Run()
        {
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

        private static async Task Initialize()
        {
            await Logger.LogAsync("Initializing");
            client = new DiscordShardedClient(new DiscordSocketConfig
            {
                DefaultRetryMode = RetryMode.RetryRatelimit,
                MessageCacheSize = 10000,
                TotalShards = 4,
               // ShardId = 1
            });

            string token;
            using (var reader = new FileReader().GetFileReader("token.txt"))
            {
                token = reader.ReadToEnd();
            }

            IServiceProvider services = InstallCommands();
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            CommandHandler manager = services.GetService<CommandHandler>();
            await manager.Start();
            await Task.Delay(-1);
        }

        private static IServiceProvider InstallCommands()
        {
            map = new ServiceCollection();
            map.AddSingleton<IWordsDataAccess, WordsDataAccess>();
            map.AddSingleton<IHangmanState, HangmanState>();
            map.AddSingleton<IHangmanLogic, HangmanLogic>();
            map.AddSingleton(client);
            map.AddSingleton(new AudioService());
            map.AddSingleton<CommandHandler>();

            var provider = new DefaultServiceProviderFactory().CreateServiceProvider(map);
            return provider;
        }
    }
}
