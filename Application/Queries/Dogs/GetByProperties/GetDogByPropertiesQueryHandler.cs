using Application.Dtos;
using Application.Queries.Dogs.GetByProperties;
using Infrastructure.Database;
using MediatR;
using System.Data.Entity;

public class GetDogsByPropertiesQueryHandler : IRequestHandler<GetDogsByPropertiesQuery, List<DogDto>>
{
    private readonly RealDatabase _dbContext;

    public GetDogsByPropertiesQueryHandler(RealDatabase dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<DogDto>> Handle(GetDogsByPropertiesQuery request, CancellationToken cancellationToken)
    {
        // Implementera logiken för att hämta hundar baserat på Breed och/eller Weight från databasen
        // Använd _dbContext för att göra databasförfrågningar

        // Exempel:
        var dogs = await _dbContext.Dogs
            .Where(d => (request.Breed == null || d.Breed == request.Breed) &&
                        (!request.Weight.HasValue || d.Weight > request.Weight))
            .Select(d => new DogDto
            {
                // Mappa relevanta egenskaper här...
            })
            .ToListAsync();

        return dogs;
    }
}
