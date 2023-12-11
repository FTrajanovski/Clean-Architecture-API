using Application.Commands.Dogs.UpdateDog;
using Application.Commands.Dogs;
using Application.Commands.Dogs.AddDog;
using Application.Commands.Dogs.DeleteDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Validators.Dog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.DogsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        internal readonly DogValidator _dogValidator;

        // Konstruktor som tar in en instans av IMediator (MediatR används för att implementera CQRS-mönstret)
        public DogsController(IMediator mediator, DogValidator dogValidator)
        {
            _mediator = mediator;
            _dogValidator = dogValidator;
        }

        // // Hämta alla hundar från databasen
        [HttpGet]
        [Route("getAllDogs")]
        public async Task<IActionResult> GetAllDogs()
        {
            try
            {
                // Försök hämta alla hundar från databasen
                var dogs = await _mediator.Send(new GetAllDogsQuery());

                // Returnera hundarna om hämtningen lyckades
                return Ok(dogs);
            }
            catch (Exception ex)
            {
                // Om det uppstår ett fel, returnera ett felmeddelande
                return BadRequest($"Error getting dogs: {ex.Message}");
            }
        }

        //  // Hämta en hund med ett specifikt ID
        [HttpGet]
        [Route("getDogById/{dogId}")]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            // Validera Dog Id
            var validatedDogId = new GuidValidator().Validate(dogId);

            // Felhantering för ogiltigt Dog Id
            if (!validatedDogId.IsValid)
            {
                return BadRequest(validatedDogId.Errors.Select(error => error.ErrorMessage));
            }

            // Försök hämta hunden med det specifika ID:et
            try
            {
                var dog = await _mediator.Send(new GetDogByIdQuery(dogId));

                // Om hunden inte finns, returnera NotFound
                if (dog == null)
                {
                    return NotFound("Dog not found.");
                }

                // Returnera hunden om hämtningen lyckades
                return Ok(dog);
            }
            catch (Exception ex)
            {
                // Felhantering med ett generiskt felmeddelande
                return BadRequest($"Error getting dog: {ex.Message}");
            }
        }

        // // Skapa en ny hund
        [HttpPost]
        [Route("addNewDog")]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog)
        {
            //Validate Dog
            var validatedDog = _dogValidator.Validate(newDog);
            
            //Error handling
            if (!validatedDog.IsValid)
            {
                return BadRequest(validatedDog.Errors.ConvertAll(errors => errors.ErrorMessage));
            }
            
            //Try Catch
            try
            {
                return Ok(await _mediator.Send(new AddDogCommand(newDog)));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        // Update a specific dog
        [HttpPut]
        [Route("updateDog/{dogId}")]
        public async Task<IActionResult> UpdateDog(Guid dogId, [FromBody] DogDto updatedDog)
        {
            // Validate Dog ID
            var validatedDogId = new GuidValidator().Validate(dogId);
            if (!validatedDogId.IsValid)
            {
                return BadRequest(validatedDogId.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            // Validate Updated Dog
            var validatedUpdatedDog = _dogValidator.Validate(updatedDog);
            if (!validatedUpdatedDog.IsValid)
            {
                return BadRequest(validatedUpdatedDog.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            // Try Catch
            try
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
            catch (Exception ex)
            {
                // Här kan du använda ditt eget felmeddelande för exceptions om du vill
                return BadRequest($"Error updating dog: {ex.Message}");
            }
        }

        // Radera en specifik hund, om tasken går igenom returnera "Dog deleted sucessfully" om inte "Dog not found".
        [HttpDelete]
        [Route("deleteDog/{dogId}")]
        public async Task<IActionResult> DeleteDog(Guid dogId)
        {
            var validatedDogId = new GuidValidator().Validate(dogId);
            if (!validatedDogId.IsValid)
            {
                foreach (var error in validatedDogId.Errors)
                {
                    ModelState.AddModelError(error.ErrorCode, error.ErrorMessage);
                }

                return BadRequest(ModelState);
            }

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
