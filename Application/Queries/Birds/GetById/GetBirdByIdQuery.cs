using Domain.Models;
using MediatR;


namespace Application.Queries.Birds.GetById
{
    public class GetBirdByIdQuery : IRequest<Bird>
    {
        // En konstruktor som tar en Guid (unikt ID) som parameter och tilldelar det privata fältet Id
        public GetBirdByIdQuery(Guid birdId)
        {
            Id = birdId;
        }

        public Guid Id { get; }
    }


}
