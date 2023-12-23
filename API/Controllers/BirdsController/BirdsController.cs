using Application.Commands.Birds;
using Application.Commands.Birds.AddBird;
using Application.Commands.Birds.DeleteBird;
using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Application.Queries.Birds.GetBirdsByColor;
using Application.Queries.Birds.GetById;
using Application.Validators.Bird;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Lägg till detta
using System;
using System.Threading.Tasks;

namespace API.Controllers.BirdsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        private readonly ILogger<BirdsController> _logger; // Lägg till logger

        private readonly BirdValidator _birdValidator;
        private readonly GuidValidator _guidValidator;
        private readonly GetBirdsByColorValidator _getBirdsByColorValidator;

        // Konstruktor som tar en instans av IMediator och valideringsklasserna
        public BirdsController(
            IMediator mediator,
            ILogger<BirdsController> logger, // Lägg till logger
            BirdValidator birdValidator,
            GuidValidator guidValidator,
            GetBirdsByColorValidator getBirdsByColorValidator)
        {
            _mediator = mediator;
            _logger = logger; // Lägg till logger
            _birdValidator = birdValidator;
            _guidValidator = guidValidator;
            _getBirdsByColorValidator = getBirdsByColorValidator;
        }

        // ...

        [HttpGet]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds()
        {
            try
            {
                _logger.LogInformation("Fetching all birds from the database.");
                var birds = await _mediator.Send(new GetAllBirdsQuery());
                _logger.LogInformation("Successfully retrieved all birds.");
                return Ok(birds);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching all birds: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // ...

        [HttpPost]
        [Route("addNewBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            try
            {
                // Validera fågeln
                var validationResult = _birdValidator.Validate(newBird);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning($"Failed to add a new bird due to validation errors: {string.Join(", ", validationResult.Errors)}");
                    return BadRequest(validationResult.Errors);
                }

                _logger.LogInformation($"Adding a new bird: {newBird.Name}");
                return Ok(await _mediator.Send(new AddBirdCommand(newBird)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding a new bird: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // ...

        [HttpPut]
        [Route("updateBird/{birdId}")]
        public async Task<IActionResult> UpdateBird(Guid birdId, [FromBody] BirdDto updatedBird)
        {
            try
            {
                // Validera ID
                var idValidationResult = _guidValidator.Validate(birdId);
                if (!idValidationResult.IsValid)
                {
                    _logger.LogWarning($"Failed to update bird with ID {birdId} due to validation errors: {string.Join(", ", idValidationResult.Errors)}");
                    return BadRequest(idValidationResult.Errors);
                }

                // Validera fågeln
                var birdValidationResult = _birdValidator.Validate(updatedBird);
                if (!birdValidationResult.IsValid)
                {
                    _logger.LogWarning($"Failed to update bird with ID {birdId} due to validation errors: {string.Join(", ", birdValidationResult.Errors)}");
                    return BadRequest(birdValidationResult.Errors);
                }

                var command = new UpdateBirdByIdCommand(updatedBird, birdId);
                var result = await _mediator.Send(command);

                if (result != null)
                {
                    _logger.LogInformation($"Successfully updated bird with ID {birdId}");
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning($"Failed to update bird with ID {birdId} because it was not found.");
                    return NotFound("Bird not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating bird with ID {birdId}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // ...

        [HttpDelete]
        [Route("deleteBird/{birdId}")]
        public async Task<IActionResult> DeleteBird(Guid birdId)
        {
            try
            {
                // Validera ID
                var idValidationResult = _guidValidator.Validate(birdId);
                if (!idValidationResult.IsValid)
                {
                    _logger.LogWarning($"Failed to delete bird with ID {birdId} due to validation errors: {string.Join(", ", idValidationResult.Errors)}");
                    return BadRequest(idValidationResult.Errors);
                }

                var success = await _mediator.Send(new DeleteBirdCommand(birdId));

                if (success)
                {
                    _logger.LogInformation($"Successfully deleted bird with ID {birdId}");
                    return Ok("Bird deleted successfully.");
                }
                else
                {
                    _logger.LogWarning($"Failed to delete bird with ID {birdId} because it was not found.");
                    return NotFound("Bird not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting bird with ID {birdId}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // ...

        [HttpGet]
        [Route("getBirdsByColor")]
        public async Task<IActionResult> GetBirdsByColor([FromQuery] GetBirdsByColorQuery query)
        {
            try
            {
                // Validera query
                var validationResult = _getBirdsByColorValidator.Validate(query);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning($"Failed to fetch birds by color due to validation errors: {string.Join(", ", validationResult.Errors)}");
                    return BadRequest(validationResult.Errors);
                }

                var result = await _mediator.Send(query);
                _logger.LogInformation($"Successfully retrieved birds by color: {query.Color}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching birds by color: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
