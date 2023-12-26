using Domain.Models.UserAnimal;
using Infrastructure.Repositories.Animals;
using MediatR;

namespace Application.Commands.UserAnimals.DeleteConnection
{
    public class DeleteAnimalUserConnectionCommandHandler : IRequestHandler<DeleteAnimalUserConnectionCommand, UserAnimalJointTable>
    {
        private readonly IAnimalRepository _animalRepository;

        public DeleteAnimalUserConnectionCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public Task<UserAnimalJointTable> Handle(DeleteAnimalUserConnectionCommand request, CancellationToken cancellationToken)
        {
            var connectionToDelete = _animalRepository.DeleteConnection(request.UserId, request.AnimalId);

            return connectionToDelete;
        }
    }
}