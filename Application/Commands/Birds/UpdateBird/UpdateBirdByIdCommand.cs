using Application.Dtos;
using Domain.Models;
using MediatR;
using System;

// Skapa ett kommando (UpdateBirdByIdCommand) för att begära uppdatering av fågel baserat på ID
namespace Application.Commands.Birds.UpdateBird
{
    // Implementera IRequest-gränssnittet för att hantera uppdatering av fågelkommandot och returnera en fågelmodell
    public class UpdateBirdByIdCommand : IRequest<Bird>
    {
        // Konstruktor för att skapa ett uppdaterat fågelobjekt och behålla ID
        public UpdateBirdByIdCommand(BirdDto updatedBird, Guid id)
        {
            UpdatedBird = updatedBird;
            Id = id;
        }

        // Egenskap för att hålla det uppdaterade fågelobjektet
        public BirdDto UpdatedBird { get; }

        // Egenskap för att hålla fågelns ID som ska uppdateras
        public Guid Id { get; }
    }

}