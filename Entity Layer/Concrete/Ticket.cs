using System.ComponentModel.DataAnnotations;

namespace Entity_Layer.Concrete
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        public int AssignedId { get; set; }
        public User? AssignedUser { get; set; }

        public bool IsTester { get; set; }
        public string Tester { get; set; }

        public int Priority { get; set; }
        public DateTime EstimatedTime { get; set; }
        public string? Description { get; set; }
        public string? Reporter{ get; set; }

        public string? TicketType { get; set; }
        public string? ProjectName { get; set; }
        public List<TestCase>? TestCases { get; set; }

        public int ColumnId { get; set; }
        public Column? Column { get; set; }

    }
}
