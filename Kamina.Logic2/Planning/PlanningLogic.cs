using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kamina.Contracts.DataAccess;
using Kamina.Contracts.Logic;
using Kamina.Contracts.Objects;

namespace Kamina.Logic.Planning
{
    public class PlanningLogic : IPlanningLogic
    {
        private readonly IKaminaDbContext _context;

        public PlanningLogic(IKaminaDbContext context)
        {
            _context = context;
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
