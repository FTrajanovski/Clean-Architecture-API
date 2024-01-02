using API.Controllers.DogsController;
using Application.Commands.Dogs.DeleteDog;
using Application.Dtos;
using Application.Validators;
using Application.Validators.Dog;
using Domain.Models;
using FakeItEasy;
using Infrastructure.Repositories.Dogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class DeleteDogTests
    {
        [Test]
        public async Task Controller_Delete_Dog()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var dog = new DogDto { Name = "Hasse", Weight = 1, Breed = "Tax" };

            var mediator = A.Fake<IMediator>();

            A.CallTo(() => mediator.Send(guid, CancellationToken.None)).Returns(dog);

            var guidValidator = new GuidValidator();

            var dogValidator = new DogValidator();

            var dogController = new DogsController(mediator, dogValidator, guidValidator);

            //Act
            var result = await dogController.DeleteDog(guid);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);

        }

        [Test]
        public async Task Handle_Delete_Correct_Id()
        {
            //Arrange
            var dog = new Dog { Name = "Fredrik", AnimalId = Guid.NewGuid(), Breed = "Tax", Weight = 100 };

            var dogRepository = A.Fake<IDogRepository>();

            var handler = new DeleteDogByIdCommandHandler(dogRepository);

            A.CallTo(() => dogRepository.DeleteDogById(dog.AnimalId)).Returns(dog);

            var command = new DeleteDogByIdCommand(dog.AnimalId);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name.Equals(dog.Name));
            Assert.That(result, Is.TypeOf<Dog>());
            Assert.That(result.AnimalId.Equals(dog.AnimalId));
        }
    }
}