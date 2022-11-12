using System.ComponentModel.DataAnnotations;


namespace Entity_Layer.Concrete
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool isAdmin { get; set; } = true;
    }
}
