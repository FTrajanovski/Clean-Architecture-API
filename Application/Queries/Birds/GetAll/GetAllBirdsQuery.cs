using Domain.Models;
using MediatR;


namespace Application.Queries.Birds.GetAll
{
    //Hämtar alla fåglar genom att ärva ifrån listan.
    public class GetAllBirdsQuery : IRequest<List<Bird>>
    {
    }
}
