using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.UserAnimals.AddUserAnimal
{
    public class AddUserAnimalCommandHandler : IRequestHandler<AddUserAnimalCommand, bool>
    {
        private readonly RealDatabase _dbContext;

        public AddUserAnimalCommandHandler(RealDatabase dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(AddUserAnimalCommand request, CancellationToken cancellationToken)
        {
            var userAnimal = new UserAnimal
            {
                UserId = request.UserAnimal.UserId,
                AnimalId = request.UserAnimal.AnimalId
            };

            _dbContext.UserAnimals.Add(userAnimal);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}