using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Infrastructure.Database.Repositories.UserRepo;
using Infrastructure.Database.Repositories.AnimalRepo;



namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {



            services.AddScoped<IDogRepository, DogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAnimalRepository, UserAnimalRepository>();

            services.AddSingleton<MockDatabase>();

            services.AddDbContext<RealDatabase>(options =>
            {
                string connectionString = "Server=localhost;Port=3306;Database=CleanAPI;User=root;Password=Kadino44;";
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 35)));

            });
            return services;

        }



    }
}