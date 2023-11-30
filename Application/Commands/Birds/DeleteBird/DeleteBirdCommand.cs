
using MediatR;
using System;

// Skapa ett kommando (DeleteBirdCommand) för att begära borttagning av en fågel med ett specifikt ID
namespace Application.Commands.Birds.DeleteBird
{
    // Implementera IRequest-gränssnittet för att skicka ett kommando för att radera en fågel
    public class DeleteBirdCommand : IRequest<bool>
    {
        // Konstruktor för att skapa en instans av DeleteBirdCommand med det unika ID:t för fågeln som ska raderas
        public DeleteBirdCommand(Guid birdId)
        {
            BirdId = birdId;
        }

        // Egenskap för att hämta fågelns ID
        public Guid BirdId { get; }
    }
}
