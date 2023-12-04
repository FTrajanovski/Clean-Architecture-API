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
            // Initera handler och mockdatabasen inför varje test
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
            Assert.That(result.Name, Is.EqualTo(newBirdDto.Name));

            // Kontrollera om fågeln har lagts till i databasen
            Assert.Contains(result, _mockDatabase.Birds);
        }
    }
}
