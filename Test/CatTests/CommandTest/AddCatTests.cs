using Application.Commands.Cats.AddCat;
using Application.Dtos;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class AddCatTests
    {
        private AddCatCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new AddCatCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidData_AddsToDatabase()
        {
            // Arrange
            var newCatDto = new CatDto { Name = "Kattis" };
            var command = new AddCatCommand(newCatDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Name, Is.EqualTo(newCatDto.Name));

            // Check if the cat was added to the database
            Assert.Contains(result, _mockDatabase.Cats);
        }
    }
}
