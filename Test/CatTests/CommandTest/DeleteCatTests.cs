using API.Controllers.CatsController;
using Application.Commands.Cats.DeleteCat;
using Application.Dtos;
using Application.Validators;
using Application.Validators.Cat;
using Domain.Models;
using FakeItEasy;
using Infrastructure.Repositories.Cats;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class DeleteCatTests
    {
        [Test]
        public async Task Controller_Delete_Cat()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var cat = new CatDto { Name = "Morris", Breed = "Huskatt", LikesToPlay = true, Weight = 4 };

            var mediator = A.Fake<IMediator>();

            A.CallTo(() => mediator.Send(guid, CancellationToken.None)).Returns(cat);

            var guidValidator = new GuidValidator();

            var catValidator = new CatValidator();

            var catController = new CatsController(mediator, catValidator, guidValidator);

            //Act

            var result = await catController.DeleteCat(guid);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Handle_Delete_Correct_Id()
        {

            //Arrange
            var cat = new Cat
            {
                Name = "Hans",
                AnimalId = Guid.NewGuid()
            };

            var catRepository = A.Fake<ICatRepository>();

            var handler = new DeleteCatByIdCommandHandler(catRepository);

            A.CallTo(() => catRepository.DeleteCatById(cat.AnimalId)).Returns(cat);


            var command = new DeleteCatByIdCommand(cat.AnimalId);

            //Act

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name.Equals("Hans"));
            Assert.That(result, Is.TypeOf<Cat>());
            Assert.That(result.AnimalId.Equals(cat.AnimalId));
        }
    }
}