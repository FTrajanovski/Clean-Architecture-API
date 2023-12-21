using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatCommandHandler : IRequestHandler<DeleteCatCommand, bool>
    {
        private readonly RealDatabase _realDatabase;
        private MockDatabase mockDatabase;

        public DeleteCatCommandHandler(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public DeleteCatCommandHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public Task<bool> Handle(DeleteCatCommand request, CancellationToken cancellationToken)
        {
            var catToRemove = _realDatabase.Cats.FirstOrDefault(cat => cat.Id == request.CatId);

            if (catToRemove != null)
            {
                _realDatabase.Cats.Remove(catToRemove);
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}