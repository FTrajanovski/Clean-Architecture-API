using API.Controllers.CatsController;
using Application.Commands.Cats.UpdateCat;
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
    public class UpdateCatTests
    {
        [Test]
        public async Task Controller_Update_Cat()
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

            var result = await catController.UpdateCatById(cat, guid);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Handle_Update_Correct_Cat_By_Id()
        {
            //Arrange

            var guid = new Guid("ce9b91e4-08d1-4628-82c1-8ef6ec622220");

            var cat = new Cat { AnimalId = new Guid("ce9b91e4-08d1-4628-82c1-8ef6ec622220"), Name = "Hans", LikesToPlay = true };

            var catDto = new CatDto { Name = "Carl", LikesToPlay = true };

            var catRepository = A.Fake<ICatRepository>();

            var handler = new UpdateCatByIdCommandHandler(catRepository);

            A.CallTo(() => catRepository.GetCatById(cat.AnimalId)).Returns(cat);

            A.CallTo(() => catRepository.UpdateCat(cat)).Returns(cat);

            var command = new UpdateCatByIdCommand(catDto, guid);

            //Act

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name.Equals("Carl"));
            Assert.That(result.LikesToPlay.Equals(true));
            Assert.That(result, Is.TypeOf<Cat>());
        }
    }
}