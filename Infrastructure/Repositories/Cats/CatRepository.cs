using Domain.Models;
using Domain.Models.UserAnimal;
using Infrastructure.Database.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Cats
{
    public class CatRepository : ICatRepository
    {
        private readonly SqlDatabase _sqlDatabase;

        public CatRepository(SqlDatabase sqlDatabase)
        {
            _sqlDatabase = sqlDatabase;
        }

        public async Task<Cat> AddCat(Cat newCat, Guid id)
        {
            try
            {
                var user = await _sqlDatabase.Users.FirstOrDefaultAsync(x => x.UserId == id);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var userAnimal = new UserAnimalJointTable { AnimalId = newCat.AnimalId, UserId = id };

                _sqlDatabase.UserAnimals.Add(userAnimal);

                _sqlDatabase.Cats.Add(newCat);
                _sqlDatabase.SaveChanges();
                return await Task.FromResult(newCat);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<Cat> DeleteCatById(Guid id)
        {
            try
            {
                Cat catToDelete = await GetCatById(id);

                UserAnimalJointTable connectionToRemove = await _sqlDatabase.UserAnimals.Where(c => c.AnimalId == catToDelete.AnimalId).FirstOrDefaultAsync();

                if (connectionToRemove != null)
                {
                    _sqlDatabase.UserAnimals.Remove(connectionToRemove);
                }


                _sqlDatabase.Cats.Remove(catToDelete);

                _sqlDatabase.SaveChanges();

                return await Task.FromResult(catToDelete);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while deleting a cat with Id {id} from the database", ex);
            }
        }

        public async Task<List<Cat>> GetAllCatsAsync()
        {

            try
            {
                return await _sqlDatabase.Cats.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while getting all cats from the database", ex);
            }

        }

        public async Task<List<Cat>> GetCatsByWeightBreed(int? weight, string? breed)
        {
            try
            {
                var query = _sqlDatabase.Cats.OfType<Cat>();

                if (weight.HasValue)
                {
                    query = query.Where(cat => cat.Weight >= weight.Value);
                }

                if (!string.IsNullOrEmpty(breed))
                {
                    query = query.Where(cat => cat.Breed == breed);
                }

                var cats = await query.OrderByDescending(cat => cat.Weight).ToListAsync();

                return cats;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cat> GetCatById(Guid id)
        {
            try
            {
                Cat? wantedCat = await _sqlDatabase.Cats.FirstOrDefaultAsync(cat => cat.AnimalId == id);

                if (wantedCat == null)
                {
                    throw new Exception($"There was no cat with Id {id} in the database");
                }

                return await Task.FromResult(wantedCat);

            }
            catch (Exception ex)
            {

                throw new Exception($"An error occured while getting a cat with Id {id} from database", ex);
            }
        }

        public Task<Cat> UpdateCat(Cat updateCat)
        {
            try
            {
                _sqlDatabase.Cats.Update(updateCat);

                _sqlDatabase.SaveChanges();

                return Task.FromResult(updateCat);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while updating a cat by Id {updateCat.AnimalId} from database", ex);
            }
        }
    }
}