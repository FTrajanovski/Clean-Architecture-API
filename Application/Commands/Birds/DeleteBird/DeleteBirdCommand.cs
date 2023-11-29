using MediatR;
using System;

namespace Application.Commands.Birds.DeleteBird
{
    public class DeleteBirdCommand : IRequest<bool>
    {
        public DeleteBirdCommand(Guid birdId)
        {
            BirdId = birdId;
        }

        public Guid BirdId { get; }
    }
}