using Application.Dtos;
using MediatR;

namespace Application.Queries.Dogs.GetByProperties
{
    public class GetDogsByPropertiesQuery : IRequest<List<DogDto>>
    {
        public string? Breed { get; set; }
        public int? Weight { get; set; }
    }


    }
