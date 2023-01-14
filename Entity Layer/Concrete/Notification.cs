using System.ComponentModel.DataAnnotations;

namespace Entity_Layer.Concrete
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public string? NotificationShortut { get; set; }
        public string? NotificationTarget { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public bool isRead { get; set; }
        public bool isUpdated { get; set; }
    }
}
