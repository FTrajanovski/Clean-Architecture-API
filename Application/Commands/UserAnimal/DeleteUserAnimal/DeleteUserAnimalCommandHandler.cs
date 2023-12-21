using Application.Commands.UserAnimals.DeleteUserAnimal;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class DeleteUserAnimalCommandHandler : IRequestHandler<DeleteUserAnimalCommand, bool>
{
    private readonly RealDatabase _dbContext;

    public DeleteUserAnimalCommandHandler(RealDatabase dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteUserAnimalCommand request, CancellationToken cancellationToken)
    {
        var userAnimalToDelete = await _dbContext.UserAnimals
            .FirstOrDefaultAsync(ua => ua.UserId == request.UserId && ua.AnimalId == request.AnimalId);

        if (userAnimalToDelete != null)
        {
            _dbContext.UserAnimals.Remove(userAnimalToDelete);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }
}