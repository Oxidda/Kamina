using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace Kamina.Logic.Commands
{
    public class Waifu : ModuleBase
    {
        [Command("waifustats")]
        public async Task WaifuStats()
        {
            try
            {
                await ReplyAsync($"{this.Context.User.Mention} I am your waifu <3");
            }
            catch (Exception)
            {

            }
        }
    }
}
