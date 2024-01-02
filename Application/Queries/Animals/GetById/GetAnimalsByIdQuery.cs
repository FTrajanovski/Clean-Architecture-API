using Domain.Models.UserAnimal;
using MediatR;

namespace Application.Queries.Animals.GetById
{
    public class GetAnimalsByIdQuery : IRequest<AnimalUserModel>
    {
        public GetAnimalsByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}