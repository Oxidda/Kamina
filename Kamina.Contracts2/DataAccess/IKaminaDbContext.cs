using System.Threading;
using System.Threading.Tasks;
using Kamina.Contracts.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Kamina.Contracts.DataAccess
{
    public interface IKaminaDbContext
    {
        DbSet<CalenderItem> CalenderItems { get; set; }
        DbSet<Guild> Guilds { get; set; }
        DbSet<User> Users { get; set; }

        Task<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
