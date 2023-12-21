using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserAnimals.UpdateUserAnimal
{
    public class UpdateUserAnimalCommand : IRequest<bool>
    {
        public UserAnimalDto UserAnimal { get; }
        public Guid UserId { get; }
        public Guid AnimalId { get; }

        public UpdateUserAnimalCommand(UserAnimalDto userAnimal, Guid userId, Guid animalId)
        {
            UserAnimal = userAnimal;
            UserId = userId;
            AnimalId = animalId;
        }
    }
}
