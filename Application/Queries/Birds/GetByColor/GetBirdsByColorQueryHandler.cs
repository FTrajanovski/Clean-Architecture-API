using Application.Dtos;
using Application.Queries.Birds.GetBirdsByColor;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Birds
{
    public class GetBirdsByColorQueryHandler : IRequestHandler<GetBirdsByColorQuery, List<BirdDto>>
    {
        private readonly RealDatabase _context;

        public GetBirdsByColorQueryHandler(RealDatabase context)
        {
            _context = context;
        }

        public async Task<List<BirdDto>> Handle(GetBirdsByColorQuery request, CancellationToken cancellationToken)
        {
            var birdsQuery = _context.Birds.AsQueryable();

            if (!string.IsNullOrEmpty(request.Color))
            {
                birdsQuery = birdsQuery.Where(b => b.Color == request.Color);
            }

            var birds = await birdsQuery.OrderByDescending(b => b.Name).Select(b => new BirdDto
            {
                Name = b.Name,
                CanFly = b.CanFly,
                Color = b.Color
            }).ToListAsync(cancellationToken);

            return birds;
        }
    }
}
