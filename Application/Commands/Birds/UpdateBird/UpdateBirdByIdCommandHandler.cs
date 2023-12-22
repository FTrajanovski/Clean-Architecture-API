using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// Implementera en hanterare (UpdateBirdByIdCommandHandler) för att behandla begäran om att uppdatera en fågel baserat på ID
namespace Application.Commands.Birds.UpdateBird
{
    // Implementera IRequestHandler-gränssnittet för att hantera uppdatering av fågelkommandon och returnera en fågelmodell
    public class UpdateBirdByIdCommandHandler : IRequestHandler<UpdateBirdByIdCommand, Bird>
    {
        // Privat fält för att hålla en databasreferens
        private readonly RealDatabase _realDatabase;
        private MockDatabase mockDatabase;

        // Konstruktor för att ta emot en mockdatabasreferens och tilldela den privata variabeln
        public UpdateBirdByIdCommandHandler(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        // Metod för att hantera uppdateringsbegäran för fågelkommandot
        public Task<Bird> Handle(UpdateBirdByIdCommand request, CancellationToken cancellationToken)
        {
            // Hämta fågeln som ska uppdateras baserat på ID från mockdatabasen
            Bird birdToUpdate = _realDatabase.Birds.FirstOrDefault(bird => bird.Id == request.Id)!;

            // Uppdatera fågelns namn och flygförmåga med de nya värdena från kommandot
            birdToUpdate.Name = request.UpdatedBird.Name;
            birdToUpdate.CanFly = request.UpdatedBird.CanFly;

            // Returnera den uppdaterade fågeln
            return Task.FromResult(birdToUpdate);
        }
    }
}