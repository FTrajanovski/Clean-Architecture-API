using Domain.Models.Account;
using Domain.Models.Animal;
using Domain.Models.UserAnimal;
using Infrastructure.Database.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Animals
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly SqlDatabase _sqlDatabase;

        public AnimalRepository(SqlDatabase sqlDatabase)
        {
            _sqlDatabase = sqlDatabase;
        }

        public Task<UserAnimalJointTable> AddConnection(Guid userId, Guid animalId)
        {
            try
            {
                var connectionToCreate = new UserAnimalJointTable
                {
                    UserId = userId,
                    AnimalId = animalId
                };

                _sqlDatabase.UserAnimals.Add(connectionToCreate);

                _sqlDatabase.SaveChanges();

                return Task.FromResult(connectionToCreate);
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occured when adding a connection between user with Id {userId} and animal with Id {animalId}", ex);
            }

        }

        public async Task<UserAnimalJointTable> DeleteConnection(Guid userId, Guid animalId)
        {
            try
            {
                UserAnimalJointTable connectionToDelete = await _sqlDatabase.UserAnimals.Where(u => u.UserId == userId).Where(a => a.AnimalId == animalId).FirstOrDefaultAsync();

                if (connectionToDelete == null)
                {
                    throw new Exception("There is no user with that animal in the database");
                }

                _sqlDatabase.UserAnimals.Remove(connectionToDelete);

                _sqlDatabase.SaveChanges();

                return await Task.FromResult(connectionToDelete);
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occured while deleting the connection between {userId} and {animalId}", ex);
            }
        }

        public async Task<List<AnimalUserModel>> GetAllAnimals()
        {
            try
            {
                List<AnimalUserModel> allAnimals = new List<AnimalUserModel>();

                var dogs = await _sqlDatabase.Dogs.ToListAsync();

                var birds = await _sqlDatabase.Birds.ToListAsync();

                var cats = await _sqlDatabase.Cats.ToListAsync();

                foreach (var dog in dogs)
                {
                    var animal = new AnimalUserModel { AnimalId = dog.AnimalId, AnimalName = dog.Name, Breed = dog.Breed };

                    List<UserModelForAnimals> userModelForAnimals = new List<UserModelForAnimals>();

                    var users = await _sqlDatabase.UserAnimals.ToListAsync();

                    foreach (var user in users)
                    {
                        if (animal.AnimalId == user.AnimalId)
                        {
                            var wantedUser = new UserModelForAnimals { UserId = user.UserId };
                            animal.Users.Add(wantedUser);
                        }
                    }


                    allAnimals.Add(animal);
                }

                foreach (var bird in birds)
                {
                    var animal = new AnimalUserModel { AnimalId = bird.AnimalId, AnimalName = bird.Name, Breed = "Bird" };

                    List<UserModelForAnimals> userModelForAnimals = new List<UserModelForAnimals>();

                    var users = await _sqlDatabase.UserAnimals.ToListAsync();

                    foreach (var user in users)
                    {
                        if (animal.AnimalId == user.AnimalId)
                        {
                            var wantedUser = new UserModelForAnimals { UserId = user.UserId };
                            animal.Users.Add(wantedUser);
                        }
                    }

                    allAnimals.Add(animal);
                }

                foreach (var cat in cats)
                {
                    var animal = new AnimalUserModel { AnimalId = cat.AnimalId, AnimalName = cat.Name, Breed = cat.Breed };

                    List<UserModelForAnimals> userModelForAnimals = new List<UserModelForAnimals>();

                    var users = await _sqlDatabase.UserAnimals.ToListAsync();

                    foreach (var user in users)
                    {
                        if (animal.AnimalId == user.AnimalId)
                        {
                            var wantedUser = new UserModelForAnimals { UserId = user.UserId };
                            animal.Users.Add(wantedUser);
                        }
                    }

                    allAnimals.Add(animal);
                }

                return allAnimals;

            }
            catch (Exception ex)
            {

                throw new Exception("An error occured while getting all animals in database", ex);
            }
        }

        public async Task<UserAnimalModel> GetAllAnimalsForUser(Guid id)
        {
            try
            {
                List<UserAnimalJointTable> searchedAnimals = new List<UserAnimalJointTable>();

                var username = await _sqlDatabase.Users.FirstOrDefaultAsync(x => x.UserId == id);

                if (username == null)
                {
                    throw new Exception($"There is no user with id {id} in the database");
                }

                var user = new UserAnimalModel { UserId = id, Username = username.Username };

                var userAnimals = _sqlDatabase.UserAnimals.Where(user => user.UserId == id).ToList();

                foreach (var dog in userAnimals)
                {
                    var dogs = await _sqlDatabase.Dogs.FirstOrDefaultAsync(x => x.AnimalId == dog.AnimalId);

                    if (dogs == null)
                    {
                        continue;
                    }

                    var animal = new AnimalModel { AnimalId = dog.AnimalId, Name = dogs.Name };

                    user.Animals.Add(animal);
                    searchedAnimals.Add(dog);
                }

                foreach (var animals in searchedAnimals)
                {
                    userAnimals.Remove(animals);
                }

                foreach (var cat in userAnimals)
                {
                    var cats = await _sqlDatabase.Cats.FirstOrDefaultAsync(x => x.AnimalId == cat.AnimalId);

                    if (cats == null)
                    {
                        continue;
                    }
                    var animal = new AnimalModel { AnimalId = cat.AnimalId, Name = cats.Name };

                    user.Animals.Add(animal);
                }

                foreach (var animals in searchedAnimals)
                {
                    userAnimals.Remove(animals);
                }

                foreach (var bird in userAnimals)
                {
                    var birds = await _sqlDatabase.Birds.FirstOrDefaultAsync(x => x.AnimalId == bird.AnimalId);

                    if (birds == null)
                    {
                        continue;
                    }
                    var animal = new AnimalModel { AnimalId = bird.AnimalId, Name = birds.Name };

                    user.Animals.Add(animal);
                }

                return user;
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occured while getting all animals for user with id {id}", ex);
            }

        }
        public async Task<AnimalUserModel> GetAnimalById(Guid id)
        {
            try
            {
                var animals = await GetAllAnimals();

                var wantedAnimal = animals.Where(a => a.AnimalId == id).FirstOrDefault();

                if (wantedAnimal == null)
                {
                    return null!;
                }

                return wantedAnimal;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while getting an animal with Id {id} from database", ex);
            }
        }
    }
}