﻿namespace Biosero.Service.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
