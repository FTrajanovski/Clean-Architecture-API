using Application.Commands.UserAnimals.GetUserAnimal;
using Application.Dtos;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetUserAnimalCommandHandler : IRequestHandler<GetUserAnimalCommand, List<UserAnimalDto>>
{
    private readonly RealDatabase _dbContext;

    public GetUserAnimalCommandHandler(RealDatabase dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserAnimalDto>> Handle(GetUserAnimalCommand request, CancellationToken cancellationToken)
    {
        var userAnimals = await _dbContext.UserAnimals
            .Include(ua => ua.User)
            .Include(ua => ua.Animal)
            .Select(ua => new UserAnimalDto
            {
                UserId = ua.UserId,
                UserName = ua.User.UserName,
                AnimalId = ua.AnimalId,
                AnimalName = ua.Animal.Name
            })
            .ToListAsync();

        return userAnimals;
    }
}