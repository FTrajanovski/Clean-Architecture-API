using Domain.Models.Animal;

namespace Domain.Models
{
    public class UserAnimal
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid AnimalId { get; set; }
        public AnimalModel Animal { get; set; }
    }
}

