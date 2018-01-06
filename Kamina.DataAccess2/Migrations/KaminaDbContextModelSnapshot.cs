//using System;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Kamina.DataAccess;
//using Kamina.Contracts.Objects;

//namespace Kamina.DataAccess.Migrations
//{
//    [DbContext(typeof(KaminaDbContext))]
//    partial class KaminaDbContextModelSnapshot : ModelSnapshot
//    {
//        protected override void BuildModel(ModelBuilder modelBuilder)
//        {
//            modelBuilder
//                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

//            modelBuilder.Entity("Kamina.Contracts.Objects.CalenderItem", b =>
//                {
//                    b.Property<Guid>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<string>("Description");

//                    b.Property<DateTime?>("EndDate");

//                    b.Property<ulong?>("GuildId");

//                    b.Property<int>("Recurrance");

//                    b.Property<DateTime>("StartDate");

//                    b.Property<ulong?>("UserId");

//                    b.HasKey("Id");

//                    b.HasIndex("GuildId");

//                    b.HasIndex("UserId");

//                    b.ToTable("CalenderItems");
//                });

//            modelBuilder.Entity("Kamina.Contracts.Objects.Guild", b =>
//                {
//                    b.Property<ulong>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<string>("GuildName");

//                    b.HasKey("Id");

//                    b.ToTable("Guilds");
//                });

//            modelBuilder.Entity("Kamina.Contracts.Objects.User", b =>
//                {
//                    b.Property<ulong>("Id")
//                        .ValueGeneratedOnAdd();

//                    b.Property<Guid?>("CalenderItemId");

//                    b.Property<ulong?>("GuildId");

//                    b.Property<string>("Name");

//                    b.HasKey("Id");

//                    b.HasIndex("CalenderItemId");

//                    b.HasIndex("GuildId");

//                    b.ToTable("Users");
//                });

//            modelBuilder.Entity("Kamina.Contracts.Objects.CalenderItem", b =>
//                {
//                    b.HasOne("Kamina.Contracts.Objects.Guild", "Guild")
//                        .WithMany()
//                        .HasForeignKey("GuildId");

//                    b.HasOne("Kamina.Contracts.Objects.User")
//                        .WithMany("CalenderItems")
//                        .HasForeignKey("UserId");
//                });

//            modelBuilder.Entity("Kamina.Contracts.Objects.User", b =>
//                {
//                    b.HasOne("Kamina.Contracts.Objects.CalenderItem")
//                        .WithMany("Users")
//                        .HasForeignKey("CalenderItemId");

//                    b.HasOne("Kamina.Contracts.Objects.Guild", "Guild")
//                        .WithMany()
//                        .HasForeignKey("GuildId");
//                });
//        }
//    }
//}
