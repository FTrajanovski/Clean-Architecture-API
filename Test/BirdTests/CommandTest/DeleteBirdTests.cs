using API.Controllers.BirdsController;
using Application.Commands.Birds.DeleteBird;
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
    public class DeleteBirdTests
    {
        [Test]
        public async Task Controller_Delete_Bird()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var bird = new BirdDto { Name = "Hasse", CanFly = true, Color = "Green" };

            var mediator = A.Fake<IMediator>();

            A.CallTo(() => mediator.Send(guid, CancellationToken.None)).Returns(bird);

            var guidValidator = new GuidValidator();

            var birdValidator = new BirdValidator();

            var birdController = new BirdsController(mediator, birdValidator, guidValidator);

            //Act
            var result = await birdController.DeleteBird(guid);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);

        }

        [Test]
        public async Task Handle_Delete_Correct_Id()
        {
            //Arrange
            var bird = new Bird { Name = "Kaja", Color = "Red", AnimalId = Guid.NewGuid(), CanFly = true };

            var birdRepository = A.Fake<IBirdRepository>();

            var handler = new DeleteBirdByIdCommandHandler(birdRepository);

            A.CallTo(() => birdRepository.DeleteBirdById(bird.AnimalId)).Returns(bird);

            var command = new DeleteBirdByIdCommand(bird.AnimalId);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name.Equals("Kaja"));
            Assert.That(result, Is.TypeOf<Bird>());
            Assert.That(result.AnimalId.Equals(bird.AnimalId));
        }
    }
}