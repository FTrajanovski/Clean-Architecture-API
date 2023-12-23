namespace Domain.Models
{
    public class User : UserModel
    {
        public bool IsAdmin { get; set; }
        public ICollection<UserAnimal> UserAnimals { get; set; } = new List<UserAnimal>();
    }
}
