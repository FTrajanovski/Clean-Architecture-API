using Application.Dtos;
using Domain.Models;
using MediatR;

// Implementera ett kommando (AddCatCommand) för att lägga till en ny katt och returnera kattmodellen
namespace Application.Commands.Cats.AddCat
{
    // Implementera IRequest-gränssnittet för att definiera ett lägga-till-kattkommando
    public class AddCatCommand : IRequest<Cat>
    {
        // Konstruktor för att ta emot en ny kattmodell och tilldela den privata variabeln
        public AddCatCommand(CatDto newCat)
        {
            NewCat = newCat;
        }

        // Egenskap för att hålla den nya kattmodellen
        public CatDto NewCat { get; }
    }
}