using MediatR;
using System.Collections.Generic;
using Application.Dtos;

namespace Application.Commands.UserAnimals.GetUserAnimal
{
    public class GetUserAnimalCommand : IRequest<List<UserAnimalDto>>
    {
    }
}


