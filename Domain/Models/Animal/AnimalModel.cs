using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Animal
{

    public class AnimalModel
    {
        [Key]
        public Guid AnimalId { get; set; }
        public string Name { get; set; }
    }
}