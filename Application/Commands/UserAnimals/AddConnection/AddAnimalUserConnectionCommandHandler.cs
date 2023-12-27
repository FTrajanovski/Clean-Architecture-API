using Domain.Models.UserAnimalModel;
using Infrastructure.Repositories.Animals;
using MediatR;

namespace Application.Commands.UserAnimals.AddConnection
{
    public class AddAnimalUserConnectionCommandHandler : IRequestHandler<AddAnimalUserConnectionCommand, UserAnimalJointTable>
    {
        private readonly IAnimalRepository _animalRepository;
        public AddAnimalUserConnectionCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public Task<UserAnimalJointTable> Handle(AddAnimalUserConnectionCommand request, CancellationToken cancellationToken)
        {

            var connectionToCreate = _animalRepository.AddConnection(request.UserId, request.AnimalId);


            return connectionToCreate;
        }
    }
}