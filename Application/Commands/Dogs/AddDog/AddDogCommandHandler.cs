using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Dogs.AddDog
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly RealDatabase _realDatabase;
        private MockDatabase mockDatabase;

        public AddDogCommandHandler(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public AddDogCommandHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            Dog dogToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name
            };

            _realDatabase.Dogs.Add(dogToCreate);

            return Task.FromResult(dogToCreate);
        }
    }
}
