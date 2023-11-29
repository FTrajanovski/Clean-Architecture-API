using Application.Commands.Dogs.DeleteDog;
using Infrastructure.Database;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class DeleteDogTests
    {
        private DeleteDogCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new DeleteDogCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ExistingDogId_RemovesFromDatabase()
        {
            // Arrange
            var existingDogId = _mockDatabase.Dogs.First().Id; // Assuming there's at least one dog in the database
            var command = new DeleteDogCommand(existingDogId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(_mockDatabase.Dogs.Any(d => d.Id == existingDogId));
        }

        [Test]
        public async Task Handle_NonExistingDogId_ReturnsFalse()
        {
            // Arrange
            var nonExistingDogId = Guid.NewGuid(); // Assuming this ID does not exist in the database
            var command = new DeleteDogCommand(nonExistingDogId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
