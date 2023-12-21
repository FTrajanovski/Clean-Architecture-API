namespace Domain.Models.Animal
{
    public class AnimalModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation property for the many-to-many relationship
        public ICollection<UserAnimal> UserAnimals { get; set; } = new List<UserAnimal>();
    }
}

