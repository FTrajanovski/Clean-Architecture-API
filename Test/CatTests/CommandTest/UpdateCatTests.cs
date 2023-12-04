using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Infrastructure.Database;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class UpdateCatTests
    {
        private UpdateCatByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new UpdateCatByIdCommandHandler(_mockDatabase);
        }


        [Test]
        public async Task Handle_ExistingCatId_UpdatesName()
        {
            // Arrange
            var existingCatId = _mockDatabase.Cats.First().Id; // Assuming there's at least one cat in the database
            var updatedCatDto = new CatDto { Name = "UpdatedName" };
            var command = new UpdateCatByIdCommand(updatedCatDto, existingCatId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Name, Is.EqualTo(updatedCatDto.Name));
            Assert.That(result.Id, Is.EqualTo(existingCatId));
        }
    }
}