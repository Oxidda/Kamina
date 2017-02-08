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
            //if (HangmanState.State.Keys.All(x => x != Context.Guild.Id))
            //{
            //    var client = (Context.Client as DiscordSocketClient);
            //    var state = new HangManGame(client);
            //    state.TargetWord = HangmanState.GetRandomWord().ToLower();
            //    HangmanState.State.Add(Context.Guild.Id, state);

            //    await state.Start(Context);
            //}
            //else
            //{
            //    await ReplyAsync("Galgje draait al. Believe in yourself, je kunt het raden!!");
            //}
        }

        private IHangmanLogic logic;
    }
}