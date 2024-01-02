using Domain.Models.Animal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("Dog")]
    public class Dog : AnimalModel
    {
        public string Bark()
        {
            return "This animal barks";
        }

        public int Weight { get; set; }

        public string Breed { get; set; }
    }
}