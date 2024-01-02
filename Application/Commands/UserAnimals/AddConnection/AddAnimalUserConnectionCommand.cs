using Domain.Models.UserAnimal;
using MediatR;

namespace Application.Commands.UserAnimals.AddConnection
{
    public class AddAnimalUserConnectionCommand : IRequest<UserAnimalJointTable>
    {

        public AddAnimalUserConnectionCommand(Guid userId, Guid animalId)
        {
            UserId = userId;
            AnimalId = animalId;
        }

        public Guid UserId { get; }
        public Guid AnimalId { get; }
    }
}