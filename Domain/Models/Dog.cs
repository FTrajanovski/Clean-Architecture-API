using Domain.Models.Animal;

namespace Domain.Models
{
    public class Dog : AnimalModel
    {
        public string? Breed { get; set; }
        public int Weight { get; set; }

        public string Bark()
        {
            return "This animal barks";
        }
        
    }
}
