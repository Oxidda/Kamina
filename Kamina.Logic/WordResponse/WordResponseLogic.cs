using Kamina.Contracts.Logic;
using Kamina.Contracts.Objects;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kamina.Logic.WordResponse
{
    public class WordResponseLogic : IWordResponseLogic
    {
        private readonly ConcurrentDictionary<string, TextResponse> _euroRegExs;
        private readonly ConcurrentDictionary<string, TextResponse> _words;

        public WordResponseLogic()
        {
            TextResponse vijfEuroResponse = new TextResponse
            {
                ShouldMentionSender = true,
                Text = "5 euro? OP JE MUIL, GAUW!"
            };
            _euroRegExs = new ConcurrentDictionary<string, TextResponse>();
            _euroRegExs.TryAdd(@"^(?=.*\b5\b)(?=.*\beuro\b).*$", vijfEuroResponse);
            _euroRegExs.TryAdd(@"^(?=.*\b5euro\b).*$", vijfEuroResponse);
            _euroRegExs.TryAdd(@"^(?=.*\beuro5\b).* $", vijfEuroResponse);
            _euroRegExs.TryAdd(@"[€]\s*([5]+?)", vijfEuroResponse);

            _words = new ConcurrentDictionary<string, TextResponse>();
            _words.TryAdd("sheet", new TextResponse
            {
                ShouldMentionSender = true,
                Text = "Geen zorgen, ik maak er wel een private sheet van!"
            });

            var jaspertje = new TextResponse
            {
                ShouldMentionSender = true,
                Text = "Je bent toch geen Jaspertje van plan he?"
            };

            _words.TryAdd("vluchten", jaspertje);
            _words.TryAdd("vlucht", jaspertje);
            _words.TryAdd("wegrennen", jaspertje);

            var vechten = new TextResponse
            {
                ShouldMentionSender = true,
                Text = "Kom jij met al je vrienden, dan kom ik ook alleen!"
            };

            _words.TryAdd("vechten", vechten);
            _words.TryAdd("cloth", new TextResponse
            {
                ShouldMentionSender = true,
                Text = "Hoorde ik daar cloth? Kan je terugkopen op de auction."
            });

            _words.TryAdd("spoon", new TextResponse
            {
                ShouldMentionSender = true,
                PersonToMention = 172319870266900480,
                Text = "IS GEEN LITTLE SPOON"
            });

            _words.TryAdd("lepeltje", new TextResponse
            {
                ShouldMentionSender = true,
                PersonToMention = 172319870266900480,
                Text = "IS GEEN LITTLE SPOON"
            });

            _words.TryAdd("lepel", new TextResponse
            {
                ShouldMentionSender = true,
                PersonToMention = 172319870266900480,
                Text = "IS GEEN LITTLE SPOON"
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
                     foreach (var word in _words)
                     {
                         if (text.ToLower().Contains(word.Key))
                         {
                             TextResponse response;
                             if (_words.TryGetValue(word.Key, out response))
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
                foreach (var regEx in _euroRegExs)
                {
                    if (Regex.IsMatch(text, regEx.Key))
                    {
                        TextResponse response;
                        _euroRegExs.TryGetValue(regEx.Key, out response);
                        return response;
                    }
                }

                return null;
            });
        }
    }
}
