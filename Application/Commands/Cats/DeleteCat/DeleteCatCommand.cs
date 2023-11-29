using MediatR;
using System;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatCommand : IRequest<bool>
    {
        public DeleteCatCommand(Guid catId)
        {
            CatId = catId;
        }

        public Guid CatId { get; }
    }
}