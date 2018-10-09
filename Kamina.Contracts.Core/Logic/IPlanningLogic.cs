using System.Collections.Generic;
using System.Threading.Tasks;
using Kamina.Contracts.Core.Objects;

namespace Kamina.Contracts.Core.Logic
{
    public interface IPlanningLogic
    {
        Task<bool> PlanAsync(CalenderItem item);
        Task<List<CalenderItem>> PlanningAsync(User user);
    }
}
