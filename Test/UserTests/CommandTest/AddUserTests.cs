//using Application.Commands.User.AddUser;
//using Application.Dtos;
//using Infrastructure.Database;
//using NUnit.Framework;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Test.UserTests.CommandTest
//{
//    [TestFixture]
//    public class AddUserTests
//    {
//        private AddUserCommandHandler _handler;
//        private MockDatabase _mockDatabase;

//        [SetUp]
//        public void SetUp()
//        {
//            // Initera handler och mockdatabasen inför varje test
//            _mockDatabase = new MockDatabase();
//            _handler = new AddUserCommandHandler(_mockDatabase);
//        }

//        [Test]
//        public async Task Handle_ValidData_AddsToDatabase()
//        {
//            // Arrange
//            var newUserDto = new UserDto { UserName = "JohnDoe" };
//            var command = new AddUserCommand(newUserDto);

//            // Act
//            var result = await _handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.That(result.UserName, Is.EqualTo(newUserDto.UserName));

//            // Kontrollera om användaren har lagts till i databasen
//            Assert.Contains(result, _mockDatabase.Users);
//        }
//    }
//}

//// Liknande tester för UpdateUserCommandHandler, DeleteUserCommandHandler, etc.
