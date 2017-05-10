using System.Threading.Tasks;
using Discord.Commands;

namespace Kamina.Contracts.Logic
{
    public interface IHangmanLogic
    {
        Task Run(CommandContext context);
    }
}
