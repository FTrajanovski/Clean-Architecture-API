﻿namespace Application.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
        public bool Authorized { get; set; }

    }
}