﻿using System.ComponentModel.DataAnnotations;

namespace Entity_Layer.Concrete
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? Firstname { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Title { get; set; }
        public string? Password { get; set; }
        public List<Ticket>? Tickets { get; set; }
        public bool isAdmin { get; set; } = false;
        public List<Notification>? Notifications { get; set; }
    }
}
