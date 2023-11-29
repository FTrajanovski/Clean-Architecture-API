using Application.Commands.Birds.DeleteBird;
using Infrastructure.Database;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class DeleteBirdTests
    {
        private DeleteBirdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new DeleteBirdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ExistingBirdId_RemovesFromDatabase()
        {
            // Arrange
            var existingBirdId = _mockDatabase.Birds.First().Id; // Assuming there's at least one bird in the database
            var command = new DeleteBirdCommand(existingBirdId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(_mockDatabase.Birds.Any(b => b.Id == existingBirdId));
        }

        [Test]
        public async Task Handle_NonExistingBirdId_ReturnsFalse()
        {
            // Arrange
            var nonExistingCatId = Guid.NewGuid(); // Assuming this ID does not exist in the database
            var command = new DeleteBirdCommand(nonExistingCatId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
