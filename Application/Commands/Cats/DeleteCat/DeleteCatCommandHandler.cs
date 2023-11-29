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
        private readonly MockDatabase _mockDatabase;

        public DeleteCatCommandHandler(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public Task<bool> Handle(DeleteCatCommand request, CancellationToken cancellationToken)
        {
            var catToRemove = _mockDatabase.Cats.FirstOrDefault(cat => cat.Id == request.CatId);

            if (catToRemove != null)
            {
                _mockDatabase.Cats.Remove(catToRemove);
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}