using System.Threading.Tasks;
using Discord.Commands;

namespace Kamina.Contracts.Core.Logic
{
    public interface IHangmanLogic
    {
        Task Run(ICommandContext context);
    }
}
