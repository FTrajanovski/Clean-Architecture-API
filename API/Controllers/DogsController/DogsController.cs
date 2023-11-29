using Application.Commands.Cats.UpdateCat;
using Application.Commands.Dogs;
using Application.Commands.Dogs.AddDog;
using Application.Commands.Dogs.DeleteDog;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.DogsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        internal readonly IMediator _mediator;

        // Konstruktor som tar in en instans av IMediator (MediatR används för att implementera CQRS-mönstret)
        public DogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // // Hämta alla hundar från databasen
        [HttpGet]
        [Route("getAllDogs")]
        public async Task<IActionResult> GetAllDogs()
        {
            return Ok(await _mediator.Send(new GetAllDogsQuery()));
            //return Ok("GET ALL DOGS");
        }

        //  // Hämta en hund med ett specifikt ID
        [HttpGet]
        [Route("getDogById/{dogId}")]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            return Ok(await _mediator.Send(new GetDogByIdQuery(dogId)));
        }

        // // Skapa en ny hund
        [HttpPost]
        [Route("addNewDog")]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog)
        {
            return Ok(await _mediator.Send(new AddDogCommand(newDog)));
        }

        // Update a specific dog
        [HttpPut]
        [Route("updateDog/{dogId}")]
        public async Task<IActionResult> UpdateDog(Guid dogId, [FromBody] DogDto updatedDog)
        {
            var command = new UpdateDogByIdCommand(updatedDog, dogId);
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("Dog not found.");
            }
        }

        // Radera en specifik hund, om tasken går igenom returnera "Dog deleted sucessfully" om inte "Dog not found".
        [HttpDelete]
        [Route("deleteDog/{dogId}")]
        public async Task<IActionResult> DeleteDog(Guid dogId)
        {
            var success = await _mediator.Send(new DeleteDogCommand(dogId));

            if (success)
            {
                return Ok("Dog deleted successfully.");
            }
            else
            {
                return NotFound("Dog not found.");
            }
        }
    }
}
