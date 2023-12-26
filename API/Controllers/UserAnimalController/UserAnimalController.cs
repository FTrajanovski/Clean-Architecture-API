using Application.Commands.UserAnimals.AddUserAnimal;
using Application.Commands.UserAnimals.DeleteUserAnimal;
using Application.Commands.UserAnimals.GetUserAnimal;
using Application.Commands.UserAnimals.UpdateUserAnimal;
using Application.Dtos;
using Application.Validators.UserAnimal;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Lägg till detta
using System;
using System.Threading.Tasks;

namespace API.Controllers.UserAnimalController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAnimalController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserAnimalValidator _userAnimalValidator;
        private readonly UpdateUserAnimalValidator _updateUserAnimalValidator;
        private readonly ILogger<UserAnimalController> _logger; // Lägg till logger

        public UserAnimalController(IMediator mediator, UserAnimalValidator userAnimalValidator, UpdateUserAnimalValidator updateUserAnimalValidator, ILogger<UserAnimalController> logger)
        {
            _mediator = mediator;
            _userAnimalValidator = userAnimalValidator;
            _updateUserAnimalValidator = updateUserAnimalValidator;
            _logger = logger; // Lägg till logger
        }

        [HttpGet]
        [Route("getUserAnimals")]
        public async Task<IActionResult> GetUserAnimals()
        {
            try
            {
                _logger.LogInformation("Getting user animals from the database.");
                var result = await _mediator.Send(new GetUserAnimalCommand());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting user animals: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("addUserAnimal")]
        public async Task<IActionResult> AddUserAnimal([FromBody] UserAnimalDto userAnimalDto)
        {
            try
            {
                // Validera UserAnimalDto
                var validationResult = _userAnimalValidator.Validate(userAnimalDto);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Invalid data received while adding a user animal relationship.");
                    return BadRequest(validationResult.Errors);
                }

                _logger.LogInformation("Adding a new user animal relationship.");
                var result = await _mediator.Send(new AddAnimalUserConnectionCommand(userAnimalDto));

                if (result)
                {
                    return Ok("UserAnimal relationship added successfully.");
                }
                else
                {
                    _logger.LogWarning("Unable to add user animal relationship.");
                    return BadRequest("Unable to add UserAnimal relationship.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding a user animal relationship: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("updateUserAnimal/{userId}/{animalId}")]
        public async Task<IActionResult> UpdateUserAnimal(Guid userId, Guid animalId, [FromBody] UserAnimalDto updatedUserAnimalDto)
        {
            try
            {
                // Validera UpdateUserAnimalDto
                var validationResult = _updateUserAnimalValidator.Validate(updatedUserAnimalDto);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Invalid data received while updating a user animal relationship.");
                    return BadRequest(validationResult.Errors);
                }

                _logger.LogInformation($"Updating user animal relationship for User ID: {userId} and Animal ID: {animalId}");
                var command = new UpdateUserAnimalCommand(updatedUserAnimalDto, userId, animalId);
                var result = await _mediator.Send(command);

                if (result)
                {
                    return Ok("UserAnimal relationship updated successfully.");
                }
                else
                {
                    _logger.LogWarning($"UserAnimal relationship for User ID {userId} and Animal ID {animalId} not found.");
                    return NotFound("UserAnimal relationship not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating user animal relationship: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("deleteUserAnimal/{userId}/{animalId}")]
        public async Task<IActionResult> DeleteUserAnimal(Guid userId, Guid animalId)
        {
            try
            {
                _logger.LogInformation($"Deleting user animal relationship for User ID: {userId} and Animal ID: {animalId}");
                var success = await _mediator.Send(new DeleteAnimalUserConnectionCommand(userId, animalId));

                if (success)
                {
                    return Ok("UserAnimal relationship deleted successfully.");
                }
                else
                {
                    _logger.LogWarning($"UserAnimal relationship for User ID {userId} and Animal ID {animalId} not found.");
                    return NotFound("UserAnimal relationship not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting user animal relationship: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
