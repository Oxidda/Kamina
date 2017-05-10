using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord.WebSocket;
using Kamina.Contracts.Logic;
using Kamina.Contracts.Objects;

namespace Kamina.Logic.WordResponse
{
    public class WordResponseLogic : IWordResponseLogic
    {
        private readonly ConcurrentDictionary<string, TextResponse> euroRegExs;
        private readonly ConcurrentDictionary<string, TextResponse> words;

        public WordResponseLogic()
        {
            TextResponse vijfEuroResponse = new TextResponse
            {
                ShouldMentionSender = true,
                Text = "5 euro? OP JE MUIL, GAUW!"
            };
            euroRegExs = new ConcurrentDictionary<string, TextResponse>();
            euroRegExs.TryAdd(@"^(?=.*\b5\b)(?=.*\beuro\b).*$", vijfEuroResponse);
            euroRegExs.TryAdd(@"^(?=.*\b5euro\b).*$", vijfEuroResponse);
            euroRegExs.TryAdd(@"^(?=.*\beuro5\b).* $", vijfEuroResponse);
            euroRegExs.TryAdd(@"[€]\s*([5]+?)", vijfEuroResponse);

            words = new ConcurrentDictionary<string, TextResponse>();
            words.TryAdd("sheet", new TextResponse
            {
                ShouldMentionSender = true,
                Text = "Geen zorgen, ik maak er wel een private sheet van!"
            });

            var jaspertje = new TextResponse
            {
                ShouldMentionSender = true,
                Text = "Je bent toch geen Jaspertje van plan he?"
            };

            words.TryAdd("vluchten", jaspertje);
            words.TryAdd("vlucht", jaspertje);
            words.TryAdd("wegrennen", jaspertje);

            words.TryAdd("cloth", new TextResponse
            {
                ShouldMentionSender = true,
                Text = "Hoorde ik daar cloth? Kan je terugkopen op de auction."
            });
        }

        public async Task<TextResponse> HandleText(string text)
        {
            var response = await CheckForRegexMatch(text) ?? await CheckForWordMatch(text);

            return response;
        }

        private async Task<TextResponse> CheckForWordMatch(string text)
        {
            return await Task.Run(() =>
                 {
                     foreach (var word in words)
                     {
                         if (text.ToLower().Contains(word.Key))
                         {
                             TextResponse response;
                             if (words.TryGetValue(word.Key, out response))
                             {
                                 return response;
                             }
                         }
                     }
                     return null;
                 }
             );
        }

        private async Task<TextResponse> CheckForRegexMatch(string text)
        {
            return await Task.Run(() =>
            {
                {
                    foreach (var regEx in euroRegExs)
                    {
                        if (Regex.IsMatch(text, regEx.Key))
                        {
                            TextResponse response;
                            euroRegExs.TryGetValue(regEx.Key, out response);
                            return response;
                        }
                    }

                    return null;
                }
            });
        }
    }
}
