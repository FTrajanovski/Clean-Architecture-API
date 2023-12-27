using Domain.Models.UserAnimal;
using Infrastructure.Repositories.Animals;
using MediatR;

namespace Application.Queries.Animals.GetById
{
    public class GetAnimalsByIdQueryHandler : IRequestHandler<GetAnimalsByIdQuery, AnimalUserModel>
    {
        private readonly IAnimalRepository _animalRepository;

        public GetAnimalsByIdQueryHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<AnimalUserModel> Handle(GetAnimalsByIdQuery request, CancellationToken cancellationToken)
        {
            var animal = await _animalRepository.GetAnimalById(request.Id);

            if (animal == null)
            {
                return null!;
            }

            return animal;
        }
    }
}