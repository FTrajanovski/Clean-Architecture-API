using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Application.Dtos
{
    // En klass som representerar attributen för en fågel som kan användas för överföring av data
    public class BirdDto
    {
        // Egenskap som representerar namnet på fågeln, standardvärdet är en tom sträng
        public string Name { get; set; } = string.Empty;
        // Egenskap som representerar om fågeln kan flyga eller inte
        public bool CanFly { get; set; }
    }
}