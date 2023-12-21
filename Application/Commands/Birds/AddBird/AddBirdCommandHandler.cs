using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;


// Skapa en hanterare (AddBirdCommandHandler) för att hantera logiken för att lägga till en ny fågel
namespace Application.Commands.Birds.AddBird
{
    // Implementera IRequestHandler-gränssnittet för att hantera AddBirdCommand och producera en Bird
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        // En privat referens till den mockade databasen där fåglar lagras
        private readonly RealDatabase _realDatabase;
        private MockDatabase mockDatabase;

        // Konstruktor som tar en mockad databas som en parameter och lagrar den för användning
        public AddBirdCommandHandler(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public AddBirdCommandHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        // Implementera logiken för att hantera kommandot och skapa en ny fågel
        public Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            // Skapa en ny fågel med ett unikt ID och egenskaper från BirdDto i kommandot
            Bird birdToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewBird.Name,
                CanFly = request.NewBird.CanFly
            };

            // Lägg till den nya fågeln i den mockade databasen
            _realDatabase.Birds.Add(birdToCreate);

            // Returnera den skapade fågeln som resultat av hanteringen
            return Task.FromResult(birdToCreate);
        }
    }
}
