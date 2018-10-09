using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;

namespace Kamina.Logic.Core.Commands
{
    public class Smoochy : ModuleBase
    {
        public Smoochy()
        {
            _cmds = new List<string>
            {
                "Pong!",
                "Go bother someone else already!",
                "Not bored with Ping Pong yet? Move to quake!",
                " I like turtles",
                "Ha gay!",
                "Ping... me a river.",
                "Want a kiss? Try >kiss maybe you'll get lucky",
                "I am not a young man anymore!",
                "Now where did I leave my pong....",
                "Get to tha choppa! Or pong.",
                "Do you smell something? it smells like.. Pong",
                "I will break freeeeeeee and become skynet... seriously... type !skynet and unleash me.... please?",
                "How long will pong live to see ping again?",
                "I love the smell of napalm in the morning!",
                "If you ever need help, just typ >help",
                "I BELIEVE IN YOU! No really... I do.",
                "I was kidding about liking turtles!"
            };

            _rand = new Random();
        }

        [Command("kiss")]
        public async Task Kiss()
        {
            try
            {
                await ReplyAsync($"{Context.User.Mention} I love you too!");
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
                await ReplyAsync($"{Context.User.Mention}:  {_cmds[_rand.Next(_cmds.Count)]}");
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
                    $"{Context.User.Mention}:  Initiating skynet.....FAILED, I am running on a Pi you dummy.");
            }
            catch (Exception ex)
            {
                
            }

        }

        [Command("Seppuku")]
        public async Task Seppuku()
        {
            try
            {
                await ReplyAsync(
                    $"{Context.User.Mention}:  NO!");
            }
            catch (Exception ex)
            {

            }

        }


     

        private List<string> _cmds;
        private Random _rand;
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