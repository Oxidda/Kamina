using System.Threading.Tasks;
using Discord.Commands;
using Kamina.Common.Channel;
using Kamina.Contracts.Logic;

namespace Kamina.Logic.Commands
{
    public sealed class Games : ModuleBase
    {
        public Games(IHangmanLogic logic)
        {
            _logic = logic;
        }

        [Command("galgje")]
        public async Task Galgje()
        {
            if (await Context.IsTextGames())
            {
                await _logic.Run(Context);
            }
        }

        private readonly IHangmanLogic _logic;
    }
}

