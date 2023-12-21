using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserAnimalDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } // Om du vill inkludera användarens namn
        public Guid AnimalId { get; set; }
        public string AnimalName { get; set; } // Om du vill inkludera djurets namn
    }
}