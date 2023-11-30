using Domain.Models.Animal;


namespace Domain.Models
{
    //Ärver ifrån AnimalModel
    public class Bird : AnimalModel
    {
        public bool CanFly { get; set; }
    }
}
