//using Application.Commands.Dogs;
//using Application.Dtos;
//using Infrastructure.Database;

//namespace Test.DogTests.CommandTest
//{
//    [TestFixture]
//    public class AddDogTests
//    {
//        private AddDogCommandHandler _handler;
//        private MockDatabase _mockDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            // Initialize the handler and mock database before each test
//            _mockDatabase = new MockDatabase();
//            _handler = new AddDogCommandHandler(_mockDatabase);
//        }

//        [Test]
//        public async Task Handle_ValidData_AddsToDatabase()
//        {
//            // Arrange
//            var newDogDto = new DogDto { Name = "Filip" };
//            var command = new AddDogCommand(newDogDto);

//            // Act
//            var result = await _handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(result.Name, Is.EqualTo(newDogDto.Name));

//            // Check if the dog was added to the database
//            Assert.Contains(result, _mockDatabase.Dogs);
//        }
//    }
//}

