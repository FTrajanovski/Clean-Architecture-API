namespace Domain.Models.Animal
{
    // En klass som representerar en grundläggande djurmodell och innehåller egenskaper som alla djur delar
    public class AnimalModel
    {
        // Egenskap som håller ID för djuret
        public Guid Id { get; set; }

        // Egenskap som håller namnet för djuret med en standardvärde av en tom sträng
        public string Name { get; set; } = string.Empty;
    }
}