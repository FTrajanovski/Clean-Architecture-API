using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<MockDatabase>();
<<<<<<< Updated upstream
=======
            services.AddDbContext<RealDatabase>(options =>
            {
                ;
                string connectionString = "Server=localhost;Port=3306;Database=CleanAPI;User=root;Password=Kadino44;";
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 35)));
            });
>>>>>>> Stashed changes
            return services;
        }
    }
}
