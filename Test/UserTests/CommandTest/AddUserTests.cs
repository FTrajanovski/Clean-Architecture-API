using Application.Commands.Users.AddUser;
using Application.Dtos;
using Domain.Models;
using FakeItEasy;
using Infrastructure.Repositories.Users;

namespace Test.UserTests.CommandTest
{
    [TestFixture]
    public class AddUserTests
    {


        [Test]
        public async Task Handle_Add_User()
        {
            //Arrange
            var username = "Bert";
            var password = "password";
            var role = "admin";

            var dto = new UserDto
            {
                UserName = username,
                Password = password,
                Role = role,
                Authorized = true
            };

            var user = new User { Authorized = true, Password = password, Username = username };

            var userRepository = A.Fake<IUserRepository>();

            var handler = new AddUserCommandHandler(userRepository);

            A.CallTo(() => userRepository.AddUser(user)).Returns(user);

            var command = new AddUserCommand(username, password);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.Username.Equals(username));
            Assert.That(result, Is.TypeOf<User>());

        }

    }
}