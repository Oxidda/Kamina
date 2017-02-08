using System.Threading.Tasks;
using Discord.Commands;

namespace Kamina.Contracts
{
    public interface IHangmanLogic
    {
        Task Run(CommandContext context);
    }
}
