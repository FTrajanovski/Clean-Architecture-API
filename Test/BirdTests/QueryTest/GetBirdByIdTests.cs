using API.Controllers.BirdsController;
using Application.Queries.Birds.GetById;
using Application.Validators;
using Application.Validators.Bird;
using Domain.Models;
using FakeItEasy;
using Infrastructure.Repositories.Birds;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class GetBirdByIdTests
    {
        private BirdsController _controller;
        private IMediator _mediator;
        private GuidValidator _guidValidator;
        private BirdValidator _birdValidator;

        [SetUp]
        public void SetUp()
        {
            _mediator = A.Fake<IMediator>();
            _guidValidator = new GuidValidator();
            _birdValidator = new BirdValidator();
            _controller = new BirdsController(_mediator, _birdValidator, _guidValidator);
        }

        [Test]
        public async Task Controller_Get_Bird_By_Id()
        {
            //Arrange
            var birdId = new Guid("12345678-1234-5678-1234-567812345603");

            //Act
            var result = await _controller.GetBirdById(birdId);

            //Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Handle_ValidId_ReturnCorrectBird()
        {
            var guid = Guid.NewGuid();

            var bird = new Bird { Name = "Hans", Color = "Yellow" };

            var birdRepository = A.Fake<IBirdRepository>();

            var handler = new GetBirdByIdQueryHandler(birdRepository);

            A.CallTo(() => birdRepository.GetBirdById(guid)).Returns(bird);

            var command = new GetBirdByIdQuery(guid);

            //Act

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Bird>());
            Assert.That(result.Name.Equals("Hans"));

        }
    }
}