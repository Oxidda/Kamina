using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kamina.Contracts;
using Kamina.Contracts.Objects;

namespace Kamina.Logic.Games
{
    public class HangmanLogic : IHangmanLogic
    {
        #region ctor

        public HangmanLogic(DiscordSocketClient client, IHangmanState state)
        {
            this.client = client;
            this.client.MessageReceived += Client_MessageReceived;
            this.state = state;
        }

        #endregion

        #region public

        public async Task Run(CommandContext context)
        {
            var game = new HangmanGame();
            game.TargetWord = state.GetRandomWord();

            GuildId = context.Guild.Id;

            await state.AddGameAsync(GuildId, game);
            await SendResponse(context, game, String.Empty);
        }

        #endregion

        #region private 

        private async Task Client_MessageReceived(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message == null) return;

            var context = new CommandContext(client, message);
            await Task.Run(async () =>
            {
                var game = await state.GetGameAsync(context.Guild.Id);
                if (game != null)
                {
                    if (context.Message.Content.Length == 1)
                    {
                        var v = context.Message.Content.ToLower();


                        if (game.AlreadyHadLetters.Contains(v.ToLower()))
                        {
                            game.Mistakes++;
                            if (!await CheckForLoss(game, context))
                            {
                                await SendResponse(context, game,
                                    $"Letter {v} al gebruikt! Fout! Aantal: {game.Mistakes}");
                            }
                        }
                        else
                        {
                            game.AlreadyHadLetters += v;

                            if (game.TargetWord.Contains(v))
                            {
                                game.CorrectGuessedLetters += v;
                                if (await IsCompleted(game))
                                {
                                    await Success(game, context);
                                }
                                else
                                {
                                    await SendResponse(context, game, "");
                                }
                            }
                            else
                            {
                                game.Mistakes++;
                                if (!await CheckForLoss(game, context))
                                {
                                    await SendResponse(context, game,
                                        $"Fout! Aantal: {game.Mistakes}");
                                }
                            }
                        }
                    }
                }
            });
        }

        private async Task<bool> CheckForLoss(HangmanGame game, CommandContext context)
        {
            if (game.Mistakes == 11)
            {
                await LossResponse(game, context);
                return true;
            }
            return false;
        }

        private async Task LossResponse(HangmanGame game, CommandContext context)
        {
            await SendResponse(context, game, $"YOU DIEDED, IT WAS: {game.TargetWord}");
            await Stop(context);
        }

        private Task<string> GetStringWordResponse(HangmanGame game)
        {
            return Task.Run(() =>
            {
                var responseBuilder = new StringBuilder("`");
                for (int i = 0; i < game.TargetWord.Length; i++)

                {
                    bool isMatched = false;
                    for (int j = 0; j < game.CorrectGuessedLetters.Length; j++)
                    {
                        if (game.TargetWord[i] == game.CorrectGuessedLetters[j])
                        {
                            responseBuilder.Append($"{game.CorrectGuessedLetters[j]} ");
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
            });
        }

        private async Task Success(HangmanGame game, CommandContext context)
        {
            await SendResponse(context, game, "I KNEW I COULD BELIEVE IN YOU! YOU DID IT!!!");
            await Stop(context);
        }

        private async Task<bool> IsCompleted(HangmanGame game)
        {
            var result = await GetStringWordResponse(game);
            return !result.Contains("_");
        }

        private async Task Stop(CommandContext context)
        {
            await state.RemoveGameAsync(context.Guild.Id);
        }

        private async Task SendResponse(CommandContext context, HangmanGame game, string answer)
        {
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "Game on"
            };

            var hangman = await GetHangman(game);
            hangman += await GetStringWordResponse(game) + "\n";
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

            if (!string.IsNullOrEmpty(game.AlreadyHadLetters))
            {
                builder.AddField(x =>
                {
                    x.Name = "Used letters";
                    x.Value = game.AlreadyHadLetters ?? String.Empty;
                    x.IsInline = false;
                });
            }

            await context.Channel.SendMessageAsync("", false, builder.Build());
        }

        private Task<string> GetHangman(HangmanGame game)
        {
            return Task.Run(async () =>
            {
                var baseString = "" +
                                 "//ccc3333333..\r\n" +
                                 "//2.b....4  \r\n" +
                                 "//2......5  \r\n" +
                                 "//1.....768 \r\n" +
                                 "//1.......9 \r\n" +
                                 "//1a.....g.f  \r\n" +
                                 "//1.a.....\r\n" +
                                 "//1..a....\r\n";

                List<string> chars = new List<string> { "2", "c", "b", "3", "4", "5", "6", "7", "8", "9", "f", "g" };

                if (game.Mistakes > 0)
                {
                    baseString = baseString.Replace('1', '|');
                    baseString = baseString.Replace('a', '\\');
                    if (game.Mistakes > 1)
                    {
                        baseString = baseString.Replace('2', '|');
                        baseString = baseString.Replace('c', '-');
                        baseString = baseString.Replace('b', '/');
                        chars.Remove("2");
                        chars.Remove("c");
                        chars.Remove("b");

                        if (game.Mistakes > 2)
                        {
                            baseString = baseString.Replace('3', '-');
                            chars.Remove("-");
                            chars.Remove("3");

                            if (game.Mistakes > 3)
                            {
                                baseString = baseString.Replace('4', '|');
                                chars.Remove("4");

                                if (game.Mistakes > 4)
                                {
                                    baseString = baseString.Replace('5', 'O');
                                    chars.Remove("5");

                                    if (game.Mistakes > 5)
                                    {

                                        if (game.Mistakes == 6)
                                        {
                                            baseString = baseString.Replace('7', '.');
                                            baseString = baseString.Replace('6','|');
                                            chars.Remove("6");
                                        }
                                        else
                                        {
                                            baseString = baseString.Replace('6', '|');
                                            chars.Remove("6");
                                        }
                                        if (game.Mistakes > 6)
                                        {
                                            baseString = baseString.Replace('7', '/');
                                            chars.Remove("7");

                                            if (game.Mistakes > 7)
                                            {
                                                baseString = baseString.Replace('8', '\\');
                                                chars.Remove("8");

                                                if (game.Mistakes > 8)
                                                {
                                                    baseString = baseString.Replace('9', '|');
                                                    chars.Remove("9");

                                                    if (game.Mistakes > 9)
                                                    {
                                                        baseString = baseString.Replace('g', '/');
                                                        chars.Remove("g");

                                                        if (game.Mistakes > 10)
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

                baseString = await RemoveCharsInArrayWithEmpty(baseString, chars);
                return baseString;
            });
        }

        private Task<string> RemoveCharsInArrayWithEmpty(string baseString, List<string> chars)
        {
            return Task.Run(() =>
            {
                foreach (var str in chars)
                {
                    baseString = baseString.Replace(str, String.Empty);
                }

                return baseString;
            });
        }

        private DiscordSocketClient client;

        private ulong GuildId { get; set; }

        private IHangmanState state;
        #endregion
    }
}