
using Application.Dtos;
using Domain.Models;
using MediatR;

// Skapa ett kommando (AddBirdCommand) för att lägga till en ny fågel
namespace Application.Commands.Birds.AddBird
{
    // Implementera IRequest-gränssnittet för att indikera att detta är ett MediatR-kommando
    public class AddBirdCommand : IRequest<Bird>
    {
        // Konstruktor som tar in en BirdDto för att skapa en ny fågel
        public AddBirdCommand(BirdDto newBird)
        {
            NewBird = newBird;
        }

        // Egenskap för att hålla den nya fågeln som ska läggas till
        public BirdDto NewBird { get; }
    }
}
