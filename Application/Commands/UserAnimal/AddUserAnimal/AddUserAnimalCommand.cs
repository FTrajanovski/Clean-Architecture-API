using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserAnimals.AddUserAnimal
{
    public class AddUserAnimalCommand : IRequest<bool>
    {
        public UserAnimalDto UserAnimal { get; }

        public AddUserAnimalCommand(UserAnimalDto userAnimal)
        {
            UserAnimal = userAnimal;
        }
    }
}

