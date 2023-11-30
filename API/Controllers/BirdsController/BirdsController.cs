using Application.Commands.Birds;
using Application.Commands.Birds.AddBird;
using Application.Commands.Birds.DeleteBird;
using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Application.Queries.Birds.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.BirdsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        internal readonly IMediator _mediator;

        // Konstruktor som tar en instans av IMediator (MediatR används för att implementera CQRS-mönstret)
        public BirdsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Hämta alla fåglar från databasen
        [HttpGet]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds()
        {
            // Anropa GetAllBirdsQuery för att hämta alla fåglar från databasen
            return Ok(await _mediator.Send(new GetAllBirdsQuery()));
        }

        // Hämta en fågel med ett specifikt ID
        [HttpGet]
        [Route("getBirdById/{birdId}")]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            // Anropa GetBirdByIdQuery med det specifika ID för att hämta en fågel från databasen
            return Ok(await _mediator.Send(new GetBirdByIdQuery(birdId)));
        }

        // Skapa en ny fågel.
        [HttpPost]
        [Route("addNewBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            // Anropa AddBirdCommand för att lägga till en ny fågel i databasen
            return Ok(await _mediator.Send(new AddBirdCommand(newBird)));
        }


        // Uppdatera en specifik fågel
        [HttpPut]
        [Route("updateBird/{birdId}")]
        public async Task<IActionResult> UpdateBird(Guid birdId, [FromBody] BirdDto updatedBird)
        {
            // Anropa UpdateBirdByIdCommand med det specifika ID för att uppdatera en fågel i databasen
            var command = new UpdateBirdByIdCommand(updatedBird, birdId);
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("Bird not found.");
            }
        }


        // Ta bort en specifik fågel, om lyckat returnera "Fågeln har tagits bort" om inte "Fågeln hittades inte".
        [HttpDelete]
        [Route("deleteBird/{birdId}")]
        public async Task<IActionResult> DeleteBird(Guid birdId)
        {
            // Anropa DeleteBirdCommand med det specifika ID för att ta bort en fågel från databasen
            var success = await _mediator.Send(new DeleteBirdCommand(birdId));

            if (success)
            {
                return Ok("Bird deleted successfully.");
            }
            else
            {
                return NotFound("Bird not found.");
            }
        }
    }
}