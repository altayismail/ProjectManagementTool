using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity_Layer.Concrete
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public string? TicketIdentifier { get; set; }
        public string? Title { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
        [NotMapped]
        public bool IsTester { get; set; }
        [NotMapped]
        public string? Tester { get; set; }

        public int Priority { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime EstimatedTime { get; set; }
        public string? Description { get; set; }
        public string? Reporter{ get; set; }

        public string? TicketType { get; set; }
        public List<TestCase>? TestCases { get; set; }

        public int ColumnId { get; set; }
        public Column? Column { get; set; }

    }
}
