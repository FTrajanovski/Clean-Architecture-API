using Domain.Models.UserAnimal;
using MediatR;

namespace Application.Commands.UserAnimals.DeleteConnection
{
    public class DeleteAnimalUserConnectionCommand : IRequest<UserAnimalJointTable>
    {
        public DeleteAnimalUserConnectionCommand(Guid userId, Guid animalId)
        {
            UserId = userId;
            AnimalId = animalId;
        }

        public Guid UserId { get; }
        public Guid AnimalId { get; }
    }
}