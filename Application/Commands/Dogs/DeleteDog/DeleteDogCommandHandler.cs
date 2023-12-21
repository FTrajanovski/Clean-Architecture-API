using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogCommandHandler : IRequestHandler<DeleteDogCommand, bool>
    {
        private readonly RealDatabase _realDatabase;
        private MockDatabase mockDatabase;

        public DeleteDogCommandHandler(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public DeleteDogCommandHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public Task<bool> Handle(DeleteDogCommand request, CancellationToken cancellationToken)
        {
            var dogToRemove = _realDatabase.Dogs.FirstOrDefault(d => d.Id == request.DogId);

            if (dogToRemove != null)
            {
                _realDatabase.Dogs.Remove(dogToRemove);
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
