using Application.Commands.Cats.AddCat;
using Application.Commands.Cats.DeleteCat;
using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using Application.Validators.Cat;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Lägg till detta
using System;
using System.Threading.Tasks;

namespace API.Controllers.CatsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        private readonly CatValidator _catValidator;
        private readonly GuidValidator _guidValidator;
        private readonly ILogger<CatsController> _logger; // Lägg till logger

        // Konstruktor som tar en instans av IMediator, valideringsklasserna och logger
        public CatsController(IMediator mediator, CatValidator catValidator, GuidValidator guidValidator, ILogger<CatsController> logger)
        {
            _mediator = mediator;
            _catValidator = catValidator;
            _guidValidator = guidValidator;
            _logger = logger; // Lägg till logger
        }

        // Hämta alla katter från databasen
        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            try
            {
                _logger.LogInformation("Getting all cats from the database.");
                return Ok(await _mediator.Send(new GetAllCatsQuery()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting all cats: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Hämta en katt med ett specifikt ID
        [HttpGet]
        [Route("getCatById/{catId}")]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            try
            {
                _logger.LogInformation($"Getting cat with ID: {catId}");
                return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting cat with ID {catId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Skapa en ny katt
        [HttpPost]
        [Route("addNewCat")]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            try
            {
                // Validera katten
                var validationResult = _catValidator.Validate(newCat);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Invalid data received while adding a new cat.");
                    return BadRequest(validationResult.Errors);
                }

                _logger.LogInformation($"Adding a new cat: {newCat.Name}");
                return Ok(await _mediator.Send(new AddCatCommand(newCat)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding a new cat: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Uppdatera en specifik katt
        [HttpPut]
        [Route("updateCat/{catId}")]
        public async Task<IActionResult> UpdateCat(Guid catId, [FromBody] CatDto updatedCat)
        {
            try
            {
                // Validera ID
                var idValidationResult = _guidValidator.Validate(catId);
                if (!idValidationResult.IsValid)
                {
                    _logger.LogWarning($"Invalid cat ID received: {catId}");
                    return BadRequest(idValidationResult.Errors);
                }

                // Validera katten
                var catValidationResult = _catValidator.Validate(updatedCat);
                if (!catValidationResult.IsValid)
                {
                    _logger.LogWarning("Invalid data received while updating a cat.");
                    return BadRequest(catValidationResult.Errors);
                }

                _logger.LogInformation($"Updating cat with ID: {catId}");
                var command = new UpdateCatByIdCommand(updatedCat, catId);
                var result = await _mediator.Send(command);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning($"Cat with ID {catId} not found.");
                    return NotFound("Cat not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating cat with ID {catId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // Ta bort en specifik katt
        [HttpDelete]
        [Route("deleteCat/{catId}")]
        public async Task<IActionResult> DeleteCat(Guid catId)
        {
            try
            {
                // Validera ID
                var idValidationResult = _guidValidator.Validate(catId);
                if (!idValidationResult.IsValid)
                {
                    _logger.LogWarning($"Invalid cat ID received: {catId}");
                    return BadRequest(idValidationResult.Errors);
                }

                _logger.LogInformation($"Deleting cat with ID: {catId}");
                var success = await _mediator.Send(new DeleteCatByIdCommand(catId));

                if (success)
                {
                    return Ok("Cat deleted successfully.");
                }
                else
                {
                    _logger.LogWarning($"Cat with ID {catId} not found.");
                    return NotFound("Cat not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting cat with ID {catId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
