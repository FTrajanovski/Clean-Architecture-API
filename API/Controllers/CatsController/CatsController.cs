using Application.Commands.Birds.UpdateBird;
using Application.Commands.Cats;
using Application.Commands.Cats.AddCat;
using Application.Commands.Cats.DeleteCat;
using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.CatsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        internal readonly IMediator _mediator;

        // Konstruktor som tar en instans av IMediator (MediatR används för att implementera CQRS-mönstret)
        public CatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Hämta alla katter från databasen
        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            // Anropa GetAllCatsQuery för att hämta alla katter från databasen
            return Ok(await _mediator.Send(new GetAllCatsQuery()));
        }

        // Hämta en katt med ett specifikt ID
        [HttpGet]
        [Route("getCatById/{catId}")]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            // Anropa GetCatByIdQuery med det specifika ID för att hämta en katt från databasen
            return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));
        }

        // Skapa en ny katt
        [HttpPost]
        [Route("addNewCat")]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            // Anropa AddCatCommand för att lägga till en ny katt i databasen
            return Ok(await _mediator.Send(new AddCatCommand(newCat)));
        }

        // Uppdatera en specifik katt
        [HttpPut]
        [Route("updateCat/{catId}")]
        public async Task<IActionResult> UpdateCat(Guid catId, [FromBody] CatDto updatedCat)
        {
            // Anropa UpdateCatByIdCommand med det specifika ID för att uppdatera en katt i databasen
            var command = new UpdateCatByIdCommand(updatedCat, catId);
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("Cat not found.");
            }
        }

        // Ta bort en specifik katt, om lyckat returnera "Katten har tagits bort" om inte "Katten hittades inte".
        [HttpDelete]
        [Route("deleteCat/{catId}")]
        public async Task<IActionResult> DeleteCat(Guid catId)
        {
            // Anropa DeleteCatCommand med det specifika ID för att ta bort en katt från databasen
            var success = await _mediator.Send(new DeleteCatCommand(catId));

            if (success)
            {
                return Ok("Cat deleted successfully.");
            }
            else
            {
                return NotFound("Cat not found.");
            }
        }
    }
}
