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

        // Constructor that takes an instance of IMediator (MediatR is used to implement the CQRS pattern)
        public CatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all cats from the database
        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            return Ok(await _mediator.Send(new GetAllCatsQuery()));
        }

        // Get a cat with a specific ID
        [HttpGet]
        [Route("getCatById/{catId}")]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));
        }

        // Create a new cat
        [HttpPost]
        [Route("addNewCat")]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            return Ok(await _mediator.Send(new AddCatCommand(newCat)));
        }

        // Update a specific cat
        [HttpPut]
        [Route("updateCat/{catId}")]
        public async Task<IActionResult> UpdateCat(Guid catId, [FromBody] CatDto updatedCat)
        {
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

        // Delete a specific cat, if successful return "Cat deleted successfully" if not "Cat not found".
        [HttpDelete]
        [Route("deleteCat/{catId}")]
        public async Task<IActionResult> DeleteCat(Guid catId)
        {
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
