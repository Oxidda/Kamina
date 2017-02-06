using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;


namespace Kamina.Commands
{
    public class Games : ModuleBase
    {
        [Command("galgje")]
        public async Task Galgje()
        {
            if (HangmanStatus.State.Keys.All(x => x != Context.Guild.Id))
            {
                var client = (Context.Client as DiscordSocketClient);
                var state = new HangManGame(client);
                state.TargetWord = HangmanStatus.GetRandomWord().ToLower();
                HangmanStatus.State.Add(Context.Guild.Id, state);

                await state.Start(Context);

            }
            else
            {
                await ReplyAsync("Galgje draait al. Believe in yourself, je kunt het raden!!");
            }
        }
    }


    public static class HangmanStatus
    {
        static HangmanStatus()
        {
            isoStore = IsolatedStorageFile.GetUserStoreForApplication();
            isoStream = new IsolatedStorageFileStream("Ned.txt", FileMode.OpenOrCreate, isoStore);
            reader = new StreamReader(isoStream);
            LoadWords();
            State = new Dictionary<ulong, HangManGame>();

        }

        public static void LoadWords()
        {
            words = new List<string>();
            while (!reader.EndOfStream)
            {
                words.Add(reader.ReadLine());
            }
        }

        public static string GetRandomWord()
        {
            var rand = new Random();

            var word = words[rand.Next(words.Count)];

            if (word.Contains("ĳ"))
            {
                word = word.Replace("ĳ", "ij");
            }
            return word;
        }

        private static StreamReader reader;
        private static List<string> words;
        private static IsolatedStorageFile isoStore;
        private static IsolatedStorageFileStream isoStream;
        public static Dictionary<ulong, HangManGame> State;
        private static int MaxMistakes = 10;
    }

    public class HangManGame
    {
        public HangManGame(DiscordSocketClient client)
        {
            this.client = client;
            this.client.MessageReceived += Client_MessageReceived;
        }

        public async Task Start(CommandContext context)
        {
            var responseBuilder = GetStringWordResponse();
            GuildId = context.Guild.Id;
            await SendResponse(context, responseBuilder.ToString());
        }

        private async Task Stop(CommandContext context)
        {
            this.client.MessageReceived -= Client_MessageReceived;

            await Task.Run(() => { HangmanStatus.State.Remove(context.Guild.Id); });
        }

        private async Task SendResponse(CommandContext context, string response)
        {
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "Game on"
            };

            if (!string.IsNullOrWhiteSpace(response))
            {
                builder.AddField(x =>
                {
                    x.Name = "I BELIEVE IN YOU";
                    x.Value = response;
                    x.IsInline = false;
                });

                if (!string.IsNullOrEmpty(AlreadyHadLetters))
                {
                    builder.AddField(x =>
                    {
                        x.Name = "Gebruikte letters";
                        x.Value = AlreadyHadLetters ?? String.Empty;
                        x.IsInline = false;
                    });
                }
            }

            await context.Channel.SendMessageAsync("", false, builder.Build());
        }

        public string TargetWord { get; set; } = String.Empty;

        public string AlreadyHadLetters { get; set; } = String.Empty;

        public int ErrorMistakes { get; set; }
        private ulong GuildId { get; set; }

        private async Task Client_MessageReceived(SocketMessage arg)
        {
            // Don't handle the command if it is a system message
            var message = arg as SocketUserMessage;
            if (message == null) return;

            var context = new CommandContext(client, message);

            if (context.Guild.Id == GuildId)
            {
                if (context.Message.Content.Length == 1)
                {
                    var v = context.Message.Content.ToLower();

                    if (AlreadyHadLetters.Contains(v.ToLower()))
                    {
                        ErrorMistakes++;
                        await context.Channel.SendMessageAsync($"Letter {v} al gebruikt!");
                        await context.Channel.SendMessageAsync($"Fout! Aantal: {ErrorMistakes}");
                    }
                    else
                    {
                        AlreadyHadLetters += v;

                        if (TargetWord.Contains(v))
                        {
                            correctGuessedLetters += v;
                        }
                        else
                        {
                            ErrorMistakes++;

                            await context.Channel.SendMessageAsync($"Fout! Aantal: {ErrorMistakes}");
                        }

                        var result = GetStringWordResponse(correctGuessedLetters).ToString();
                        await SendResponse(context, result);

                        if (!result.Contains("_"))
                        {
                            await SendResponse(context, "I KNEW I COULD BELIEVE IN YOU! YOU DID IT!!!");
                            await Stop(context);
                        }
                    }

                    if (ErrorMistakes == 10)
                    {
                        await context.Channel.SendMessageAsync($"YOU DIEDED");
                        await context.Channel.SendMessageAsync($"IT WAS: {TargetWord}");
                        await this.Stop(context);
                    }
                }
            }

            await Task.Delay(1);
        }

        private StringBuilder GetStringWordResponse(string v)
        {
            var responseBuilder = new StringBuilder("`");
            for (int i = 0; i < TargetWord.Length; i++)

            {
                bool isMatched = false;
                for (int j = 0; j < v.Length; j++)
                {
                    if (TargetWord[i] == v[j])
                    {
                        responseBuilder.Append($"{v[j]} ");
                        isMatched = true;
                        break;
                    }
                }

                if (!isMatched)
                {
                    responseBuilder.Append("_ ");
                }
            }
            responseBuilder.Append("`");
            return responseBuilder;
        }

        private StringBuilder GetStringWordResponse()
        {
            var responseBuilder = new StringBuilder("`");
            for (int i = 0; i < TargetWord.Length; i++)

            {
                responseBuilder.Append("_ ");
            }
            responseBuilder.Append("`");
            return responseBuilder;
        }

        private DiscordSocketClient client;

        private string correctGuessedLetters = "";
    }

}