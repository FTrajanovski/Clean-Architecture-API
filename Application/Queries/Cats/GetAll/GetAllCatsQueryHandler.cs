using Application.Queries.Cats.GetAll;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Cats.GetAll
{
    internal sealed class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, List<Cat>>
    {
        private readonly RealDatabase _realDatabase;

        public GetAllCatsQueryHandler(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public Task<List<Cat>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
        {
            List<Cat> allCatsFromRealDatabase = new List<Cat>(_realDatabase.Cats);
            return Task.FromResult(allCatsFromRealDatabase);
        }

    }
}