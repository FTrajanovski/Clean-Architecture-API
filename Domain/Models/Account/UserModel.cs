using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Account
{

    public class UserModel
    {
        [Key]
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool Authorized { get; set; }

        public string? Role { get; set; }

    }
}