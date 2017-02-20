using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Kamina.Contracts;
using Kamina.Logic.Games;

namespace Kamina.Logic.Commands
{
    public sealed class Games : ModuleBase
    {
        public Games(IHangmanLogic logic)
        {
            this.logic = logic;
        }


        [Command("galgje")]
        public async Task Galgje()
        {
            await logic.Run(Context);
        }

        private IHangmanLogic logic;
    }
}