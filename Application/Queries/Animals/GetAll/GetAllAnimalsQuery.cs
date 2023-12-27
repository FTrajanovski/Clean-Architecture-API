using Domain.Models.UserAnimalModel;
using MediatR;

namespace Application.Queries.Animals.GetAll
{
    public class GetAllAnimalsQuery : IRequest<List<AnimalUserModel>>
    {
    }
}