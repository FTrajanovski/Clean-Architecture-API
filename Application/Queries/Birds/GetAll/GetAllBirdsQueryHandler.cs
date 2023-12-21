using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Birds.GetAll
{
    // En intern (internal) och slutlig (sealed) klass som implementerar gränssnittet IRequestHandler för att hantera GetAllBirdsQuery
    internal sealed class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {
        // Ett privat fält för att hålla en referens till MockDatabase
        private readonly RealDatabase _realDatabase;

        // En konstruktor som tar en instans av MockDatabase som parameter och tilldelar det privata fältet
        public GetAllBirdsQueryHandler(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        // En metod som implementerar IRequestHandler-gränssnittet för att hantera GetAllBirdsQuery och returnera en lista av fåglar
        public Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            List<Bird> allBirdsFromRealDatabase = _realDatabase.Birds.ToList();
            return Task.FromResult(allBirdsFromRealDatabase);
        }

    }
}