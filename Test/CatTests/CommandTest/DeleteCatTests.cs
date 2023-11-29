using Application.Commands.Cats.DeleteCat;
using Infrastructure.Database;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class DeleteCatTests
    {
        private DeleteCatCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new DeleteCatCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ExistingDogId_RemovesFromDatabase()
        {
            // Arrange
            var existingCatId = _mockDatabase.Cats.First().Id; // Assuming there's at least one cat in the database
            var command = new DeleteCatCommand(existingCatId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(_mockDatabase.Dogs.Any(c => c.Id == existingCatId));
        }

        [Test]
        public async Task Handle_NonExistingCatId_ReturnsFalse()
        {
            // Arrange
            var nonExistingCatId = Guid.NewGuid(); // Assuming this ID does not exist in the database
            var command = new DeleteCatCommand(nonExistingCatId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
