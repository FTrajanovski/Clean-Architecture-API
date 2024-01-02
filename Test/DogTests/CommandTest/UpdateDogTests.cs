using API.Controllers.DogsController;
using Application.Commands.Dogs.UpdateDog;
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
    public class UpdateDogTests
    {
        [Test]
        public async Task Controller_Update_Dog()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var userId = Guid.NewGuid();

            var dog = new DogDto { Name = "Frans", Breed = "Tax", Weight = 4 };

            var mediator = A.Fake<IMediator>();

            A.CallTo(() => mediator.Send(guid, CancellationToken.None)).Returns(dog);

            var guidValidator = new GuidValidator();

            var dogValidator = new DogValidator();

            var dogController = new DogsController(mediator, dogValidator, guidValidator);

            //Act

            var result = await dogController.UpdateDog(dog, guid);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Handle_Update_Correct_Dog_By_Id()
        {
            //Arrange

            var guid = new Guid("ce9b91e4-08d1-4628-82c1-8ef6ec622220");

            var dog = new Dog { AnimalId = new Guid("ce9b91e4-08d1-4628-82c1-8ef6ec622220"), Name = "Hans", Breed = "Schäferellerhurfanmannustavardet", Weight = 1000 };

            var dogDto = new DogDto { Name = "Carl", Breed = "Tax", Weight = 2 };

            var dogRepository = A.Fake<IDogRepository>();

            var handler = new UpdateDogByIdCommandHandler(dogRepository);

            A.CallTo(() => dogRepository.GetDogById(dog.AnimalId)).Returns(dog);

            A.CallTo(() => dogRepository.UpdateDog(dog)).Returns(dog);

            var command = new UpdateDogByIdCommand(dogDto, guid);

            //Act

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name.Equals("Carl"));
            Assert.That(result.Breed.Equals(dogDto.Breed));
            Assert.That(result, Is.TypeOf<Dog>());
        }

    }
}