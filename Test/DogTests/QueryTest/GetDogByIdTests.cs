using API.Controllers.DogsController;
using Application.Queries.Dogs.GetById;
using Application.Validators;
using Application.Validators.Dog;
using Domain.Models;
using FakeItEasy;
using Infrastructure.Repositories.Dogs;
using MediatR;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetDogByIdTests
    {
        private DogsController _controller;
        private IMediator _mediator;
        private GuidValidator _guidValidator;
        private DogValidator _dogValidator;

        [SetUp]
        public void SetUp()
        {
            _mediator = A.Fake<IMediator>();
            _guidValidator = new GuidValidator();
            _dogValidator = new DogValidator();
            _controller = new DogsController(_mediator, _dogValidator, _guidValidator);
        }

        [Test]
        public async Task Controller_Get_Dog_By_Id()
        {
            //Arrange
            var dogId = new Guid("523a0c2b-6b9b-4239-a691-495a6c5778c6");

            //Act
            var result = await _controller.GetDogById(dogId);

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task Handle_ValidId_ReturnCorrectDog()
        {
            var guid = Guid.NewGuid();

            var dog = new Dog { Name = "Sven", Breed = "EnRas", Weight = 40 };

            var dogRepository = A.Fake<IDogRepository>();

            var handler = new GetDogByIdQueryHandler(dogRepository);

            A.CallTo(() => dogRepository.GetDogById(guid)).Returns(dog);

            var command = new GetDogByIdQuery(guid);

            //Act

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Dog>());
            Assert.That(result.Name.Equals("Sven"));

        }
    }
}