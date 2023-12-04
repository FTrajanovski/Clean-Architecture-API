using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Infrastructure.Database;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class UpdateDogTests
    {
        private UpdateDogByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new UpdateDogByIdCommandHandler(_mockDatabase);
        }


        [Test]
        public async Task Handle_ExistingDogId_UpdatesName()
        {
            // Arrange
            var existingDogId = _mockDatabase.Dogs.First().Id; // Assuming there's at least one dog in the database
            var updatedDogDto = new DogDto { Name = "UpdatedName" };
            var command = new UpdateDogByIdCommand(updatedDogDto, existingDogId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Name, Is.EqualTo(updatedDogDto.Name));
            Assert.That(result.Id, Is.EqualTo(existingDogId));
        }
    }
}