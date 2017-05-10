//using Kamina.Contracts.DataAccess;
//using Kamina.Contracts.Objects;
//using Microsoft.EntityFrameworkCore;

//namespace Kamina.DataAccess
//{
//    public class KaminaDbContext : DbContext, IKaminaDbContext
//    {
//        public DbSet<CalenderItem> CalenderItems { get; set; }
//        public DbSet<Guild> Guilds { get; set; }
//        public DbSet<User> Users { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//           // optionsBuilder.UseSqlite("Filename=./kaminaappointmens.db");
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//        //    modelBuilder.Entity<CalenderItem>().HasMany(x => x.Users);
//          //  modelBuilder.Entity<User>().HasMany(x => x.CalenderItems);
//         //   modelBuilder.Entity<User>().HasOne(x => x.Guild);
//        }
//    }
//}
