﻿using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;

// Skapa en hanterare (DeleteBirdCommandHandler) för att behandla borttagning av fågelkommandot
namespace Application.Commands.Birds.DeleteBird
{
    // Implementera IRequestHandler-gränssnittet för att hantera DeleteBirdCommand och returnera ett booleskt värde
    public class DeleteBirdCommandHandler : IRequestHandler<DeleteBirdCommand, bool>
    {
        // En instans av databashanteraren (MockDatabase) injiceras genom konstruktorn
        private readonly RealDatabase _realDatabase;
        private readonly MockDatabase _mockDatabase;
        private MockDatabase mockDatabase;

        public DeleteBirdCommandHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        // Konstruktor för att injicera MockDatabase-instansen
        public DeleteBirdCommandHandler(RealDatabase realDatabase, MockDatabase mockDatabase)
        {
            _realDatabase = realDatabase;
            _mockDatabase = mockDatabase;
        }

        // Hantera borttagningen av fågeln baserat på det givna kommandot
        public Task<bool> Handle(DeleteBirdCommand request, CancellationToken cancellationToken)
        {
            // Hämta fågeln som ska tas bort från databasen baserat på ID
            var birdToRemove = _realDatabase.Birds.FirstOrDefault(bird => bird.Id == request.BirdId);

            // Om fågeln hittades i databasen
            if (birdToRemove != null)
            {
                // Ta bort fågeln från databasen
                _realDatabase.Birds.Remove(birdToRemove);
                return Task.FromResult(true); // Returnera true för att indikera att borttagningen lyckades
            }
            else
            {
                return Task.FromResult(false); // Returnera false om fågeln inte hittades i databasen
            }
        }
    }
}