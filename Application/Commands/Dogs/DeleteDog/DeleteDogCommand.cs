using MediatR;
using System;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogCommand : IRequest<bool>
    {
        public DeleteDogCommand(Guid dogId)
        {
            DogId = dogId;
        }

        public Guid DogId { get; }
    }
}

