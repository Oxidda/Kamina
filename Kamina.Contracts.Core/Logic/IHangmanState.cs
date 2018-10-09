using System.Threading.Tasks;
using Kamina.Contracts.Core.Objects;

namespace Kamina.Contracts.Core.Logic
{
    public interface IHangmanState
    {
        Task AddGameAsync(ulong id, HangmanGame game);
        Task<HangmanGame> GetGameAsync(ulong id);
        Task RemoveGameAsync(ulong id);
        string GetRandomWord();
    }
}
