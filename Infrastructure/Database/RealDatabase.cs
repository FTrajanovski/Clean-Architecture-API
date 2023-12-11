using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.Database
{
    public class RealDatabase : DbContext
    {
        public RealDatabase() { }
        public RealDatabase(DbContextOptions<RealDatabase> options): base(options)
        {

        }
        public virtual DbSet<Dog> Dogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //connectionString to Db
            string connectionString = "Server=localhost;Port=3306;Database=CleanAPI;User=root;Password=Kadino44;";

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 35)));
        }
    }
}
