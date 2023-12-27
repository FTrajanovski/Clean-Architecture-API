using Domain.Models;
using Domain.Models.UserAnimal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database.SqlServer
{
    public class SqlDatabase : DbContext
    {
        private readonly IConfiguration _configuration;

        public SqlDatabase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Bird> Birds { get; set; }
        public virtual DbSet<Dog> Dogs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Cat> Cats { get; set; }
        public virtual DbSet<UserAnimalJointTable> UserAnimals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DatabaseConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<UserAnimalJointTable>(x =>
            {
                x.ToView("View_UserAnimalsJointTable");
            });
            */
            modelBuilder.Entity<UserAnimalJointTable>().ToTable(nameof(UserAnimalJointTable));

            /*
            modelBuilder.Entity<AnimalModel>()
                .UseTpcMappingStrategy()
                .Property(e => e.AnimalId)
                .HasDefaultValueSql("NEXT VALUE FOR [AnimalId]");
            modelBuilder.Entity<User>()
                .ToTable("Users");
            modelBuilder.Entity<Cat>()
                .ToTable("Cats");
            modelBuilder.Entity<Bird>()
                .ToTable("Birds");
            modelBuilder.Entity<Dog>()
                .ToTable("Dogs");
            modelBuilder.Entity<AnimalUserModel>()
                .ToTable("AnimalUserJointTable");


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AnimalModel>()
                .HasKey(a => a.AnimalId);

            modelBuilder.Entity<UserModel>()
                .HasKey(a => a.UserId);

            modelBuilder.Entity<AnimalUserModel>()
                .HasKey(ua => new { ua.AnimalId, ua.UserId });

            modelBuilder.Entity<AnimalUserModel>()
                .HasOne(ua => ua.UserModel)
                .WithMany(u => u.AnimalUsers)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<AnimalUserModel>()
                .HasOne(ua => ua.AnimalModel)
                .WithMany(a => a.AnimalUsers)
                .HasForeignKey(ua => ua.AnimalId);
            /*
            modelBuilder.Entity<UserModel>()
                .HasMany(x => x.Animals)
                .WithMany(x => x.Users)
                .UsingEntity<AnimalUserModel>(
                x => x.HasOne<AnimalModel>(x => x.AnimalModel).WithMany(x => x.AnimalUsers).HasForeignKey(x => x.AnimalId),
                x => x.HasOne<UserModel>(x => x.UserModel).WithMany(x => x.AnimalUsers).HasForeignKey(x => x.UserId)
                );
            */
        }

    }
}