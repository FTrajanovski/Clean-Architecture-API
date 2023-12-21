using Application.Commands.Dogs;
using Application.Commands.Dogs.AddDog;
using Application.Commands.Dogs.DeleteDog;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetByProperties;
using Application.Queries.Dogs.GetById;
using Application.Validators.Dog;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Lägg till detta
using System;
using System.Threading.Tasks;

namespace API.Controllers.DogsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        private readonly DogValidator _dogValidator;
        private readonly GuidValidator _guidValidator;
        private readonly GetDogsByPropertiesValidator _getDogsByPropertiesValidator;
        private readonly ILogger<DogsController> _logger; // Lägg till logger

        // Konstruktor som tar in en instans av IMediator, valideringsklasserna och logger
        public DogsController(IMediator mediator, DogValidator dogValidator, GuidValidator guidValidator, GetDogsByPropertiesValidator getDogsByPropertiesValidator, ILogger<DogsController> logger)
        {
            _mediator = mediator;
            _dogValidator = dogValidator;
            _guidValidator = guidValidator;
            _getDogsByPropertiesValidator = getDogsByPropertiesValidator;
            _logger = logger; // Lägg till logger
        }

        // Hämta alla hundar från databasen
        [HttpGet]
        [Route("getAllDogs")]
        public async Task<IActionResult> GetAllDogs()
        {
            try
            {
                _logger.LogInformation("Getting all dogs from the database.");
                return Ok(await _mediator.Send(new GetAllDogsQuery()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting all dogs: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Hämta en hund med ett specifikt ID
        [HttpGet]
        [Route("getDogById/{dogId}")]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            try
            {
                _logger.LogInformation($"Getting dog with ID: {dogId}");
                return Ok(await _mediator.Send(new GetDogByIdQuery(dogId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting dog with ID {dogId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Skapa en ny hund
        [HttpPost]
        [Route("addNewDog")]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog)
        {
            try
            {
                // Validera hunden
                var validationResult = _dogValidator.Validate(newDog);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Invalid data received while adding a new dog.");
                    return BadRequest(validationResult.Errors);
                }

                _logger.LogInformation($"Adding a new dog: {newDog.Name}");
                return Ok(await _mediator.Send(new AddDogCommand(newDog)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding a new dog: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Update a specific dog
        [HttpPut]
        [Route("updateDog/{dogId}")]
        public async Task<IActionResult> UpdateDog(Guid dogId, [FromBody] DogDto updatedDog)
        {
            try
            {
                // Validera hunden
                var validationResult = _dogValidator.Validate(updatedDog);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Invalid data received while updating a dog.");
                    return BadRequest(validationResult.Errors);
                }

                _logger.LogInformation($"Updating dog with ID: {dogId}");
                var command = new UpdateDogByIdCommand(updatedDog, dogId);
                var result = await _mediator.Send(command);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning($"Dog with ID {dogId} not found.");
                    return NotFound("Dog not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating dog with ID {dogId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Radera en specifik hund
        [HttpDelete]
        [Route("deleteDog/{dogId}")]
        public async Task<IActionResult> DeleteDog(Guid dogId)
        {
            try
            {
                // Validera ID
                var validationResult = _guidValidator.Validate(dogId);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning($"Invalid dog ID received: {dogId}");
                    return BadRequest(validationResult.Errors);
                }

                _logger.LogInformation($"Deleting dog with ID: {dogId}");
                var success = await _mediator.Send(new DeleteDogCommand(dogId));

                if (success)
                {
                    return Ok("Dog deleted successfully.");
                }
                else
                {
                    _logger.LogWarning($"Dog with ID {dogId} not found.");
                    return NotFound("Dog not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting dog with ID {dogId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Hämta hundar med specifika egenskaper
        [HttpGet]
        [Route("getDogsByProperties")]
        public async Task<IActionResult> GetDogsByProperties([FromQuery] GetDogsByPropertiesQuery query)
        {
            try
            {
                // Validera query
                var validationResult = _getDogsByPropertiesValidator.Validate(query);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Invalid query received for getting dogs by properties.");
                    return BadRequest(validationResult.Errors);
                }

                _logger.LogInformation("Getting dogs by properties.");
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting dogs by properties: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
