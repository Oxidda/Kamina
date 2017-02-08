using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Kamina.Logic.Logic;

namespace Kamina.Logic.Commands
{
    public sealed class Games : ModuleBase
    {
        [Command("galgje")]
        public async Task Galgje()
        {
            if (HangmanStatus.State.Keys.All(x => x != Context.Guild.Id))
            {
                var client = (Context.Client as DiscordSocketClient);
                var state = new HangManGame(client);
                state.TargetWord = HangmanStatus.GetRandomWord().ToLower();
                HangmanStatus.State.Add(Context.Guild.Id, state);

                await state.Start(Context);

            }
            else
            {
                await ReplyAsync("Galgje draait al. Believe in yourself, je kunt het raden!!");
            }
        }
    }
}