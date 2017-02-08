using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Kamina.Logic.Logic
{
    public class HangManGame
    {
        #region ctor

        public HangManGame(DiscordSocketClient client)
        {
            this.client = client;
            this.client.MessageReceived += Client_MessageReceived;
        }

        #endregion

        #region public

        public async Task Start(CommandContext context)
        {
            var responseBuilder = GetStringWordResponse();
            GuildId = context.Guild.Id;
            await SendResponse(context, responseBuilder.ToString(), String.Empty);
        }

        public string TargetWord { get; set; } = String.Empty;

        public string AlreadyHadLetters { get; set; } = String.Empty;

        public int Mistakes { get; set; }

        #endregion

        #region private 

        private async Task Client_MessageReceived(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message == null) return;

            var context = new CommandContext(client, message);
            await Task.Run(async () =>
            {
                if (context.Guild.Id == GuildId)
                {
                    if (context.Message.Content.Length == 1)
                    {
                        var v = context.Message.Content.ToLower();

                        if (AlreadyHadLetters.Contains(v.ToLower()))
                        {
                            Mistakes++;
                            if (!await CheckForLoss(context))
                            {
                                await SendResponse(context, GetStringWordResponse(correctGuessedLetters),
                                    $"Letter {v} al gebruikt! Fout! Aantal: {Mistakes}");
                            }
                        }
                        else
                        {
                            AlreadyHadLetters += v;

                            if (TargetWord.Contains(v))
                            {
                                correctGuessedLetters += v;
                                if (IsCompleted())
                                {
                                    await Success(context);
                                }
                                else
                                {
                                    await SendResponse(context, GetStringWordResponse(correctGuessedLetters), "");
                                }
                            }
                            else
                            {
                                Mistakes++;
                                if (!await CheckForLoss(context))
                                {
                                    await SendResponse(context, GetStringWordResponse(correctGuessedLetters),
                                      $"Fout! Aantal: {Mistakes}");
                                }
                            }
                        }
                    }
                }
            });
        }

        private async Task<bool> CheckForLoss(CommandContext context)
        {
            if (Mistakes == 10)
            {
                await LossResponse(context);
                return true;
            }
            return false;
        }

        private async Task LossResponse(CommandContext context)
        {
            await SendResponse(context, GetStringWordResponse(correctGuessedLetters), $"YOU DIEDED, IT WAS: {TargetWord}");
            await Stop(context);
        }

        private string GetStringWordResponse(string guessedLetters)
        {
            var responseBuilder = new StringBuilder("`");
            for (int i = 0; i < TargetWord.Length; i++)

            {
                bool isMatched = false;
                for (int j = 0; j < guessedLetters.Length; j++)
                {
                    if (TargetWord[i] == guessedLetters[j])
                    {
                        responseBuilder.Append($"{guessedLetters[j]} ");
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
            var v = responseBuilder.ToString();
            return v;
        }

        private async Task Success(CommandContext context)
        {
            await SendResponse(context, GetStringWordResponse(correctGuessedLetters), "I KNEW I COULD BELIEVE IN YOU! YOU DID IT!!!");
            await Stop(context);
        }

        private bool IsCompleted()
        {
            var result = GetStringWordResponse(correctGuessedLetters);
            return !result.Contains("_");
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

        private async Task Stop(CommandContext context)
        {
            client.MessageReceived -= Client_MessageReceived;

            await Task.Run(() => { HangmanStatus.State.Remove(context.Guild.Id); });
        }

        private async Task SendResponse(CommandContext context, string soFarGuessedWord, string answer)
        {
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "Game on"
            };

            var hangman = GetHangman();
            hangman += soFarGuessedWord + "\n";
            hangman += answer + "\n";

            if (!string.IsNullOrEmpty(hangman))
            {
                builder.AddField(x =>
                {
                    x.Name = "I BELIEVE IN YOU";
                    x.Value = hangman;
                    x.IsInline = false;
                });
            }

            if (!string.IsNullOrEmpty(AlreadyHadLetters))
            {
                builder.AddField(x =>
                {
                    x.Name = "Used letters";
                    x.Value = AlreadyHadLetters ?? String.Empty;
                    x.IsInline = false;
                });
            }

            await context.Channel.SendMessageAsync("", false, builder.Build());
        }

        private string GetHangman()
        {
            var baseString = "" +
             "//ccc3333333  \r\n" +
             "//2 b      4  \r\n" +
             "//2        5  \r\n" +
             "//1       768 \r\n" +
             "//1         9 \r\n" +
             "//1a     g f  \r\n" +
             "//1   a       \r\n" +
             "//1      a    \r\n";

            List<string> chars = new List<string> { "2", "c", "b", "3", "4", "5", "6", "7", "8", "9", "f", "g" };

            if (Mistakes > 0)
            {
                baseString = baseString.Replace('1', '|');
                baseString = baseString.Replace('a', '\\');
                if (Mistakes > 1)
                {
                    baseString = baseString.Replace('2', '|');
                    baseString = baseString.Replace('c', '-');
                    baseString = baseString.Replace('b', '/');
                    chars.Remove("2");
                    chars.Remove("c");
                    chars.Remove("b");

                    if (Mistakes > 2)
                    {
                        baseString = baseString.Replace('3', '-');
                        chars.Remove("-");
                        chars.Remove("3");

                        if (Mistakes > 3)
                        {
                            baseString = baseString.Replace('4', '|');
                            chars.Remove("4");

                            if (Mistakes > 4)
                            {
                                baseString = baseString.Replace('5', 'O');
                                chars.Remove("5");

                                if (Mistakes > 5)
                                {
                                    baseString = baseString.Replace('6', '|');
                                    chars.Remove("6");

                                    if (Mistakes > 6)
                                    {
                                        baseString = baseString.Replace('7', '/');
                                        chars.Remove("7");

                                        if (Mistakes > 7)
                                        {
                                            baseString = baseString.Replace('8', '\\');
                                            chars.Remove("8");

                                            if (Mistakes > 8)
                                            {
                                                baseString = baseString.Replace('9', '|');
                                                chars.Remove("9");

                                                if (Mistakes > 9)
                                                {
                                                    baseString = baseString.Replace('g', '/');
                                                    chars.Remove("g");

                                                    if (Mistakes == 10)
                                                    {
                                                        baseString = baseString.Replace('f', '\\');
                                                        chars.Remove("f");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                baseString = "";
            }

            baseString = RemoveCharsInArrayWithEmpty(baseString, chars);
            return baseString;
        }

        private static string RemoveCharsInArrayWithEmpty(string baseString, List<string> chars)
        {
            foreach (var str in chars)
            {
                baseString = baseString.Replace(str, String.Empty);
            }

            return baseString;
        }

        private DiscordSocketClient client;

        private string correctGuessedLetters = "";

        private ulong GuildId { get; set; }

        #endregion
    }
}