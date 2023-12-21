using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    public class UserModel
    {
        // Egenskap som håller ID för användaren
        public Guid UserId { get; set; }

        // Egenskap som håller namnet för djuret med en standardvärde av en tom sträng
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }

