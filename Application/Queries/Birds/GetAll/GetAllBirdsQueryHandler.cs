using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Birds.GetAll
{
    // En intern (internal) och slutlig (sealed) klass som implementerar gränssnittet IRequestHandler för att hantera GetAllBirdsQuery
    internal sealed class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {
        // Ett privat fält för att hålla en referens till MockDatabase
        private readonly MockDatabase _mockDatabase;

        // En konstruktor som tar en instans av MockDatabase som parameter och tilldelar det privata fältet
        public GetAllBirdsQueryHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        // En metod som implementerar IRequestHandler-gränssnittet för att hantera GetAllBirdsQuery och returnera en lista av fåglar
        public Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            // Hämta alla fåglar från MockDatabase och returnera dem som en uppgift (Task)
            List<Bird> allBirdsFromMockDatabase = _mockDatabase.Birds;
            return Task.FromResult(allBirdsFromMockDatabase);
        }
    }
}