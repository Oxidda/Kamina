//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Discord;
//using Discord.Commands;

//namespace Kamina.Commands
//{
//    public sealed class SimpleReplyCommand : ModuleBase
//    {
//        public SimpleReplyCommand(string commandText, string prefix, string responseText)
//        {
//            CommandText = commandText;
//            Prefix = prefix;
//            ResponseText = responseText;
//        }

//        public SimpleReplyCommand(string commandText, string prefix, List<string> responseTexts)
//        {
//            if (responseTexts == null || !responseTexts.Any())
//            {
//                throw new ArgumentNullException("Responsetexts is null or empty.");
//            }

//            CommandText = commandText;
//            Prefix = prefix;
//            ResponseTexts = responseTexts;
//            MoreThanOne = true;
//        }


//        public string CommandText { get; }

//        public string Prefix { get; }

//        public string ResponseText { get; }

//        public List<string> ResponseTexts { get; }

//        private  bool MoreThanOne { get; }

//        public async void HandleCommandAsync(CommandHandlerContext context)
//        {
//            try
//            {
//                if (context.Message.Content.ToLower() == $"{Prefix}{CommandText.ToLower()}")
//                {
//                    string txt = "";
//                    if (MoreThanOne)
//                    {
//                        txt = ResponseTexts[GetRandomNumberByListCount()];
//                    }
//                    else
//                    {
//                        txt = ResponseText;
//                    }

//                    await context.Channel.SendMessageAsync(txt);
//                    context.IsHandeld = true;
//                }
//                context.IsHandeld = false;
//            }
//            catch (Exception)
//            {
                
//            }
//        }

//        private int GetRandomNumberByListCount()
//        {
//            Random rnd = new Random();
//            return rnd.Next(ResponseTexts.Count);
//        }
//    }

//    public class CommandHandlerContext : CommandContext
//    {
//        public CommandHandlerContext(IDiscordClient client, IUserMessage msg) : base(client, msg)
//        {
//        }

//        public bool IsHandeld { get; set; }
//    }

//}
