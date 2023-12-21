﻿using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Dog>
    {
        private readonly RealDatabase _realDatabase;
        private MockDatabase mockDatabase;

        public GetDogByIdQueryHandler(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public GetDogByIdQueryHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public Task<Dog> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            Dog wantedDog = _realDatabase.Dogs.FirstOrDefault(dog => dog.Id == request.Id)!;
            return Task.FromResult(wantedDog);
        }
    }
}
