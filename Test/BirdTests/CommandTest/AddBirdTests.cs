using Application.Commands.Birds.AddBird;
using Application.Dtos;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class AddBirdTests
    {
        private AddBirdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new AddBirdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidData_AddsToDatabase()
        {
            // Arrange
            var newBirdDto = new BirdDto { Name = "Flaxi" };
            var command = new AddBirdCommand(newBirdDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(newBirdDto.Name, result.Name);

            // Check if the bird was added to the database
            Assert.Contains(result, _mockDatabase.Birds);
        }

        [Test]
        public async Task Handle_InvalidData_ReturnsNull()
        {
            // Arrange
            var invalidBirdDto = new BirdDto { Name = null }; // Invalid data
            var command = new AddBirdCommand(invalidBirdDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }
    }
}
