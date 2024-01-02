using API.Controllers.BirdsController;
using Application.Commands.Birds.AddBird;
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
    public class AddBirdTests
    {

        [Test]
        public async Task Contoller_AddBird()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var userId = Guid.NewGuid();

            var bird = new BirdDto { Name = "Hans", CanFly = true, Color = "Green" };

            var mediator = A.Fake<IMediator>();

            A.CallTo(() => mediator.Send(guid, CancellationToken.None)).Returns(bird);

            var guidValidator = new GuidValidator();

            var birdValidator = new BirdValidator();

            var birdController = new BirdsController(mediator, birdValidator, guidValidator);

            //Act

            var result = await birdController.AddBird(bird, userId);

            //Assert

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

        }


        [Test]
        public async Task Handler_AddBird()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var bird = new Bird { AnimalId = guid, Name = "Pelle", Color = "Green" };

            var birdDto = new BirdDto { Name = bird.Name, Color = bird.Color, CanFly = true };

            var birdRepository = A.Fake<IBirdRepository>();

            var handler = new AddBirdCommandHandler(birdRepository);

            A.CallTo(() => birdRepository.AddBird(bird, guid)).Returns(bird);

            var command = new AddBirdCommand(birdDto, guid);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Bird>());
            Assert.That(result.Name.Equals("Pelle"));

        }
    }
}