using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kamina.Contracts;
using Kamina.Contracts.Objects;

namespace Kamina.Logic.Games
{
    public class HangmanState : IHangmanState
    {
        public HangmanState(IWordsDataAccess dataAccess)
        {
            State = new ConcurrentDictionary<ulong, HangmanGame>();
            dataAccess.GetWordsAsync().ContinueWith(x=> { words = x.Result; });
        }
        
        public string GetRandomWord()
        {
            var rand = new Random();

            var word = words[rand.Next(words.Count)];

            if (word.Contains("ĳ"))
            {
                word = word.Replace("ĳ", "ij");
            }
            if (word.Contains("ï"))
            {
                word = word.Replace("ï", "i");
            }
            return word;
        }

        public async Task AddGameAsync(ulong id, HangmanGame game)
        {
            await Task.Run(() =>
            {
                while (!State.TryAdd(id, game))
                {
                    Task.Delay(1);
                }
            });
        }

        public async Task RemoveGameAsync(ulong id)
        {
            await Task.Run(() =>
            {
                HangmanGame game;
                while (!State.TryRemove(id, out game))
                {
                    Task.Delay(1);
                }
            });
        }

        public async Task<HangmanGame> GetGameAsync(ulong id)
        {
            return await Task.Run(() =>
            {
                if (State.ContainsKey(id))
                {
                    HangmanGame game;
                    while (!State.TryGetValue(id, out game))
                    {
                        Task.Delay(1);
                    }

                    return game;
                }
                return null;
            });
        }

        private List<string> words;
        public ConcurrentDictionary<ulong, HangmanGame> State { get; set; }
        
    }
}