using Application.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Lägg till detta
using System;
using System.Threading.Tasks;
using Application.Queries.User.GetUserById;
using Application.Commands.User.AddUser;
using Application.Commands.User.UpdateUser;
using Application.Commands.User.DeleteUser;
using Application.Commands.User.LoginUser;
using Application.Validators.User;
using Microsoft.AspNetCore.Identity;
using Application.Validators.UserAnimal;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserValidator _userValidator;
        private readonly ILogger<UsersController> _logger; // Lägg till logger

        public UsersController(IMediator mediator, UserValidator userValidator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _userValidator = userValidator;
            _logger = logger; // Lägg till logger
        }

        [HttpGet]
        [Route("getUserById/{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            try
            {
                _logger.LogInformation($"Getting user by ID: {userId}");
                return Ok(await _mediator.Send(new GetUserByIdQuery(userId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting user by ID {userId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("addNewUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDto newUser)
        {
            try
            {
                // Validera UserDto
                var validationResult = await _userValidator.ValidateAsync(newUser);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Invalid data received while adding a new user.");
                    return BadRequest(validationResult.Errors);
                }

                _logger.LogInformation("Adding a new user.");
                return Ok(await _mediator.Send(new AddUserCommand(newUser)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding a new user: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("updateUser/{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserDto updatedUser)
        {
            try
            {
                // Validera UserDto
                var validationResult = await _userValidator.ValidateAsync(updatedUser);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning($"Invalid data received while updating user with ID: {userId}");
                    return BadRequest(validationResult.Errors);
                }

                _logger.LogInformation($"Updating user with ID: {userId}");
                var command = new UpdateUserByIdCommand(updatedUser, userId);
                var result = await _mediator.Send(command);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning($"User with ID {userId} not found.");
                    return NotFound("User not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating user with ID {userId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            try
            {
                _logger.LogInformation($"Deleting user with ID: {userId}");
                var success = await _mediator.Send(new DeleteUserByIdCommand(userId));

                if (success)
                {
                    return Ok("User deleted successfully.");
                }
                else
                {
                    _logger.LogWarning($"User with ID {userId} not found.");
                    return NotFound("User not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting user with ID {userId}: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDto loginUser)
        {
            try
            {
                // Validera UserDto
                var validationResult = await _userValidator.ValidateAsync(loginUser);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Invalid data received while attempting to log in.");
                    return BadRequest(validationResult.Errors);
                }

                _logger.LogInformation("User login attempt.");
                var result = await _mediator.Send(new LoginUserCommand(loginUser));

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning("Invalid credentials during login attempt.");
                    return Unauthorized("Invalid credentials.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred during user login: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
