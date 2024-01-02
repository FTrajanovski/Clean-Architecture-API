using API.Controllers.DogsController;
using Application.Commands.Dogs;
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
    public class AddDogTests
    {
        [Test]
        public async Task Contoller_AddDog()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var userId = Guid.NewGuid();

            var dog = new DogDto { Name = "Hans" };

            var mediator = A.Fake<IMediator>();

            A.CallTo(() => mediator.Send(guid, CancellationToken.None)).Returns(dog);

            var guidValidator = new GuidValidator();

            var dogValidator = new DogValidator();

            var dogController = new DogsController(mediator, dogValidator, guidValidator);

            //Act

            var result = await dogController.AddDog(dog, userId);

            //Assert

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

        }


        [Test]
        public async Task Handler_AddDog()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var dog = new Dog { Name = "Pelle", Breed = "Tax", Weight = 19 };

            var dogRepository = A.Fake<IDogRepository>();

            var handler = new AddDogCommandHandler(dogRepository);

            A.CallTo(() => dogRepository.AddDog(dog, guid)).Returns(dog);

            var dogDto = new DogDto { Name = dog.Name, Breed = dog.Breed, Weight = dog.Weight };

            var command = new AddDogCommand(dogDto, guid);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Dog>());
            Assert.That(result.Name.Equals("Pelle"));

        }
    }
}