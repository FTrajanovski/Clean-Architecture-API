using Application.Commands.UserAnimals.UpdateUserAnimal;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class UpdateUserAnimalCommandHandler : IRequestHandler<UpdateUserAnimalCommand, bool>
{
    private readonly RealDatabase _dbContext;

    public UpdateUserAnimalCommandHandler(RealDatabase dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(UpdateUserAnimalCommand request, CancellationToken cancellationToken)
    {
        var userAnimalToUpdate = await _dbContext.UserAnimals
            .FirstOrDefaultAsync(ua => ua.UserId == request.UserId && ua.AnimalId == request.AnimalId);

        if (userAnimalToUpdate != null)
        {
            // Uppdatera logik om det behövs
            // Exempelvis, om du vill uppdatera namnet på djuret: userAnimalToUpdate.Animal.Name = request.UserAnimal.AnimalName;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }
}