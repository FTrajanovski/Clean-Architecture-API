using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserAnimals.DeleteUserAnimal
{
    public class DeleteUserAnimalCommand : IRequest<bool>
    {
        public Guid UserId { get; }
        public Guid AnimalId { get; }

        public DeleteUserAnimalCommand(Guid userId, Guid animalId)
        {
            UserId = userId;
            AnimalId = animalId;
        }
    }
}