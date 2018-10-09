using System.Threading.Tasks;
using Discord.Commands;
using Kamina.Common.Core.Channel;
using Kamina.Contracts.Core.Logic;

namespace Kamina.Logic.Core.Commands
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

