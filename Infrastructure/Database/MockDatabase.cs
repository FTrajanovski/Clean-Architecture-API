using Domain.Models;

namespace Infrastructure.Database
{
    public class MockDatabase
    {
        public List<Dog> Dogs
        {
            get { return allDogs; }
            set { allDogs = value; }
        }

        private static List<Dog> allDogs = new()
        {
            new Dog { Id = Guid.NewGuid(), Name = "Björn"},
            new Dog { Id = Guid.NewGuid(), Name = "Patrik"},
            new Dog { Id = Guid.NewGuid(), Name = "Alfred"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestDogForUnitTests"}
        };
        public List<Cat> Cats
        {
            get { return allCats; }
            set { allCats = value; }
        }

        private static List<Cat> allCats = new()
        {
           new Cat { Id = Guid.NewGuid(), Name = "Stella", LikesToPlay = true },
           new Cat { Id = Guid.NewGuid(), Name = "Fluffy", LikesToPlay = false},
           new Cat { Id = Guid.NewGuid(), Name = "Kitten", LikesToPlay = true},
           new Cat { Id = new Guid("87654321-4321-8765-4321-987654321987"), Name = "TestCatForUnitTests", LikesToPlay = true}
        };
        public List<Bird> Birds
        {
            get { return allBirds; }
            set { allBirds = value; }
        }
        private static List<Bird> allBirds = new()
    {
        new Bird { Id = Guid.NewGuid(), Name = "Flappy", CanFly = true },
        new Bird { Id = Guid.NewGuid(), Name = "Birdy", CanFly = false },
        new Bird { Id = Guid.NewGuid(), Name = "Eagle", CanFly = true },
        new Bird { Id = new Guid("11111111-1111-1111-1111-111111111111"), Name = "TestBirdForUnitTests", CanFly = true}
    };
        public List<User> Users
        {
            get { return allUsers; }
            set { allUsers = value; }
        }
        private static List<User> allUsers = new()
    {
        new User { UserId = Guid.NewGuid(), UserName = "JohnDoe", Password = "john@example.com" },
        new User { UserId = Guid.NewGuid(), UserName = "JaneDoe", Password = "jane@example.com" },
        new User { UserId = Guid.NewGuid(), UserName = "BobSmith", Password = "bob@example.com" },
        new User { UserId = new Guid("22222222-2222-2222-2222-222222222222"), UserName = "TestUserForUnitTests", Password = "testuser@example.com" }
    };
    }
}
