using System.Collections.Generic;
using System.Threading.Tasks;
using Kamina.Contracts.Core.Logic;
using Kamina.Contracts.Core.Objects;

namespace Kamina.Logic.Core.Planning
{
    public class PlanningLogic : IPlanningLogic
    {
       // private readonly IKaminaDbContext _context;

        public PlanningLogic()
        {
            //_context = context;
        }

        public async Task<bool> PlanAsync(CalenderItem item)
        {
            //var result = await _context.AddAsync(item);
            //await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CalenderItem>> PlanningAsync(User user)
        {
            return new List<CalenderItem>();
        }
    }
}
