using API.Controllers.BirdsController;
using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Application.Validators;
using Application.Validators.Bird;
using Domain.Models;
using FakeItEasy;
using Infrastructure.Repositories.Birds;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class UpdateBirdTests
    {
        [Test]
        public async Task Controller_Update_Bird()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var userId = Guid.NewGuid();

            var bird = new BirdDto { Name = "Heck", Color = "Black", CanFly = true };

            var mediator = A.Fake<IMediator>();

            A.CallTo(() => mediator.Send(guid, CancellationToken.None)).Returns(bird);

            var guidValidator = new GuidValidator();

            var birdValidator = new BirdValidator();

            var birdController = new BirdsController(mediator, birdValidator, guidValidator);

            //Act

            var result = await birdController.UpdateBirdById(bird, userId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Handle_Update_Correct_Cat_By_Id()
        {
            var guid = Guid.NewGuid();

            var bird = new Bird { AnimalId = guid, Color = "Red", Name = "Test", CanFly = true };

            var birdDto = new BirdDto { Name = "Pär", CanFly = true, Color = "Green" };

            var birdRepository = A.Fake<IBirdRepository>();

            var handler = new UpdateBirdByIdCommandHandler(birdRepository);

            A.CallTo(() => birdRepository.GetBirdById(bird.AnimalId)).Returns(bird);

            A.CallTo(() => birdRepository.UpdateBird(bird)).Returns(bird);

            var command = new UpdateBirdByIdCommand(birdDto, guid);

            //Act

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name.Equals("Pär"));
            Assert.That(result.CanFly.Equals(true));
            Assert.That(result, Is.TypeOf<Bird>());
        }
    }
}