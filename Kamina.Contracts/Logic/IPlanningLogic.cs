using System.Collections.Generic;
using System.Threading.Tasks;
using Kamina.Contracts.Objects;

namespace Kamina.Contracts.Logic
{
    public interface IPlanningLogic
    {
        Task<bool> PlanAsync(CalenderItem item);

        Task<List<CalenderItem>> PlanningAsync(User user);
    }
}
