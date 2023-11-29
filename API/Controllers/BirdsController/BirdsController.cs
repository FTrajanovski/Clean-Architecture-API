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

        // Constructor that takes an instance of IMediator (MediatR is used to implement the CQRS pattern)
        public BirdsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all birds from the database
        [HttpGet]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds()
        {
            return Ok(await _mediator.Send(new GetAllBirdsQuery()));
        }

        // Get a bird with a specific ID
        [HttpGet]
        [Route("getBirdById/{birdId}")]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            return Ok(await _mediator.Send(new GetBirdByIdQuery(birdId)));
        }

        // Create a new bird
        [HttpPost]
        [Route("addNewBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            return Ok(await _mediator.Send(new AddBirdCommand(newBird)));
        }

        
        // Update a specific bird
        [HttpPut]
        [Route("updateBird/{birdId}")]
        public async Task<IActionResult> UpdateBird(Guid birdId, [FromBody] BirdDto updatedBird)
        {
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


        // Delete a specific bird, if successful return "Bird deleted successfully" if not "Bird not found".
        [HttpDelete]
        [Route("deleteBird/{birdId}")]
        public async Task<IActionResult> DeleteBird(Guid birdId)
        {
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