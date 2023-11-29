using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Infrastructure.Database;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class UpdateBirdTests
    {
        private UpdateBirdByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new UpdateBirdByIdCommandHandler(_mockDatabase);
        }


        [Test]
        public async Task Handle_ExistingBirdId_UpdatesName()
        {
            // Arrange
            var existingBirdId = _mockDatabase.Birds.First().Id; // Assuming there's at least one dog in the database
            var updatedBirdDto = new BirdDto { Name = "UpdatedName" };
            var command = new UpdateBirdByIdCommand(updatedBirdDto, existingBirdId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(updatedBirdDto.Name, result.Name);
            Assert.AreEqual(existingBirdId, result.Id);
        }

        [Test]
        public async Task Handle_NonExistingBirdId_ReturnsNull()
        {
            // Arrange
            var nonExistingBirdId = Guid.NewGuid(); // Assuming this ID does not exist in the database
            var updatedBirdDto = new BirdDto { Name = "UpdatedName" };
            var command = new UpdateBirdByIdCommand(updatedBirdDto, nonExistingBirdId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }
    }
}