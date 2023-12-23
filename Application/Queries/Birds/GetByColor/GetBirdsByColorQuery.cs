using System.Collections.Generic;
using Application.Dtos;
using MediatR;

namespace Application.Queries.Birds.GetBirdsByColor
{
    public class GetBirdsByColorQuery : IRequest<List<BirdDto>>
    {
        public string Color { get; }

        public GetBirdsByColorQuery(string color)
        {
            Color = color;
        }
    }
}