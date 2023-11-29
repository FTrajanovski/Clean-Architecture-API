using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;

namespace Application.Commands.Birds.DeleteBird
{
    public class DeleteBirdCommandHandler : IRequestHandler<DeleteBirdCommand, bool>
    {
        private readonly MockDatabase _mockDatabase;

        public DeleteBirdCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<bool> Handle(DeleteBirdCommand request, CancellationToken cancellationToken)
        {
            var birdToRemove = _mockDatabase.Birds.FirstOrDefault(bird => bird.Id == request.BirdId);

            if (birdToRemove != null)
            {
                _mockDatabase.Birds.Remove(birdToRemove);
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}