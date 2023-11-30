using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

// Implementera en hanterare (AddCatCommandHandler) för att behandla lägga-till-kattkommandot
namespace Application.Commands.Cats.AddCat
{
    // Implementera IRequestHandler-gränssnittet för att hantera AddCatCommand och returnera en kattmodell
    public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
    {
        // Privat variabel för att hålla referensen till en mockdatabas
        private readonly MockDatabase _mockDatabase;

        // Konstruktor som tar emot en instans av mockdatabasen och tilldelar den privata variabeln
        public AddCatCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        // Metod för att hantera lägga-till-kattkommandot och returnera en kattmodell
        public Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
        {
            // Skapa en ny kattmodell med ett unikt ID och attribut från det nya kattkommandot
            Cat catToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewCat.Name,
                LikesToPlay = request.NewCat.LikesToPlay
            };

            // Lägg till den nya katten i mockdatabasen
            _mockDatabase.Cats.Add(catToCreate);

            // Returnera den nya kattmodellen som svar på kommandot
            return Task.FromResult(catToCreate);
        }
    }
}