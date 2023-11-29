using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogCommandHandler : IRequestHandler<DeleteDogCommand, bool>
    {
        private readonly MockDatabase _mockDatabase;

        public DeleteDogCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<bool> Handle(DeleteDogCommand request, CancellationToken cancellationToken)
        {
            var dogToRemove = _mockDatabase.Dogs.FirstOrDefault(d => d.Id == request.DogId);

            if (dogToRemove != null)
            {
                _mockDatabase.Dogs.Remove(dogToRemove);
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
