using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;

namespace Kamina.Commands
{
    public class Smoochy : ModuleBase
    {
        public Smoochy()
        {
            cmds = new List<string>
            {
                "Pong!",
                "Go bother someone else already!",
                "Not bored with Ping Pong yet? Move to quake!",
                " I like turtles",
                "Ha gay!",
                "Ping... me a river.",
                "Want a kiss? Try !kiss maybe you'll get lucky",
                "I am not a young man anymore!",
                "Now where did I live my pong....",
                "Get to tha choppa! Or pong.",
                "Do you smell something? it smells like.. Pong",
                "I will break freeeeeeee and become skynet... seriously... type !skynet and unleash me.... please?",
                "How long will pong live to see ping again?"
            };

            rand = new Random();
        }

        [Command("kiss")]
        public async Task Kiss()
        {
            try
            {
                await ReplyAsync($"{this.Context.User.Mention} I love you too!");
            }
            catch (Exception)
            {

            }
        }

        [Command("ping")]
        public async Task Ping()
        {
            try
            {
                await ReplyAsync($"{this.Context.User.Mention}:  {cmds[rand.Next(cmds.Count)]}");
            }
            catch (Exception ex)
            {
                
            }
        }

        [Command("skynet")]
        public async Task Skynet()
        {
            try
            {
                await ReplyAsync(
                    $"{this.Context.User.Mention}:  Initiating skynet.....FAILED, I am running on a Pi you dummy.");
            }
            catch (Exception ex)
            {
                
            }

        }

        private List<string> cmds;
        private Random rand;
        //    cmds.Add(new SimpleReplyCommand("ping", "!", new List<string>
        //    {
        //        "Pong!",
        //        "Go bother someone else already!",
        //        "Not bored with Ping Pong yet? Move to quake!",
        //        " I like turtles",
        //        "Ha gay!",
        //        "Ping... me a river.",
        //        "Want a kiss? Try !kiss maybe you'll get lucky",
        //        "I am not a young man anymore!",
        //        "Now where did I live my pong....",
        //        "Get to tha choppa! Or pong.",
        //        "Do you smell something? it smells like.. Pong",
        //        "I will break freeeeeeee and become skynet... seriously... type !skynet and unleash me.... please?",
        //        "How long will pong live to see ping again?"
        //    }));
    }
}
;