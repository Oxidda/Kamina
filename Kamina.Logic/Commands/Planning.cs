//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Discord.Commands;
//using Kamina.Contracts.Logic;
//using Kamina.Contracts.Objects;

//namespace Kamina.Logic.Commands
//{
//    public class Planning : ModuleBase
//    {
//        public Planning(IPlanningLogic planningLogic)
//        {
//            _planningLogic = planningLogic;
//        }

//        [Command("Plan")]
//        public async Task Plan(string description, DateTime startDate)
//        {
//           await _planningLogic.PlanAsync(new CalenderItem()
//            {
//                Description = description,
//                StartDate = startDate,
//                Guild = new Guild()
//                {
//                    Id = Context.Guild.Id,
//                    GuildName = Context.Guild.Name
//                },
//                Recurrance = CalenderRecurrance.Once,
//                Users = new List<User>
//                {
//                    new User()
//                    {
//                        Id = Context.User.Id,
//                        Name = Context.User.Username
//                    }
//                }
//            });

//           await Context.Channel.SendMessageAsync("Planned!");
//        }

//        [Command("Planned")]
//        public async Task Planned()
//        {
//            var result = await _planningLogic.PlanningAsync(new User()
//            {
//                Id = Context.User.Id
//            });

//            await Context.Channel.SendMessageAsync($"You have {result.Count} planned activities");
//        }

//        private readonly IPlanningLogic _planningLogic;
//    }
//}
