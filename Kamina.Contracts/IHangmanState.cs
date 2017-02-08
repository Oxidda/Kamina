using System.Threading.Tasks;
using Kamina.Contracts.Objects;

namespace Kamina.Contracts
{
    public interface IHangmanState
    {
        Task AddGameAsync(ulong id, HangmanGame game);
        Task<HangmanGame> GetGameAsync(ulong id);
        Task RemoveGameAsync(ulong id);
        string GetRandomWord();
    }
}
