using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Models.Animal;

namespace Infrastructure.Database
{
    public class RealDatabase : DbContext
    {
        public RealDatabase() { }

        public RealDatabase(DbContextOptions<RealDatabase> options) : base(options)
        {

        }

        public virtual DbSet<Dog> Dogs { get; set; }
        public virtual DbSet<Cat> Cats { get; set; }
        public virtual DbSet<Bird> Birds { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAnimal> UserAnimals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primärnyckeldefinition för UserAnimal
            modelBuilder.Entity<UserAnimal>()
                .HasKey(ua => new { ua.UserId, ua.AnimalId });

            // Övriga konfigurationer...

            // Exempel: Konfigurera förhållandet mellan UserAnimal och User
            modelBuilder.Entity<UserAnimal>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAnimals)
                .HasForeignKey(ua => ua.UserId);

            // Exempel: Konfigurera förhållandet mellan UserAnimal och AnimalModel
            modelBuilder.Entity<UserAnimal>()
                .HasOne(ua => ua.Animal)
                .WithMany()
                .HasForeignKey(ua => ua.AnimalId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //connectionString to Db
            string connectionString = "Server=localhost;Port=3306;Database=CleanAPI;User=root;Password=Kadino44;";

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 35)));
        }
    }
}
