using API.Controllers.CatsController;
using Application.Commands.Cats.AddCat;
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
    public class AddCatTests
    {

        [Test]
        public async Task Controller_Add_Cat()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var userId = Guid.NewGuid();

            var cat = new CatDto { Name = "Morris", Breed = "Huskatt", LikesToPlay = true, Weight = 4 };

            var mediator = A.Fake<IMediator>();

            A.CallTo(() => mediator.Send(guid, CancellationToken.None)).Returns(cat);

            var guidValidator = new GuidValidator();

            var catValidator = new CatValidator();

            var catController = new CatsController(mediator, catValidator, guidValidator);

            //Act
            var result = await catController.AddCat(cat, userId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Handle_Add_Cat()
        {
            //Arrange
            var cat = new Cat { Name = "Herman" };
            var requestGuid = Guid.NewGuid();

            var catRepository = A.Fake<ICatRepository>();

            var handler = new AddCatCommandHandler(catRepository);

            A.CallTo(() => catRepository.AddCat(cat, requestGuid)).Returns(cat);

            var catName = "Herman";

            var dto = new CatDto();

            dto.Name = catName;
            dto.Breed = "Huskatt";
            dto.Weight = 1;

            var command = new AddCatCommand(dto, requestGuid);

            //Act

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name.Equals("Herman"));
            Assert.That(result, Is.TypeOf<Cat>());
        }

    }
}