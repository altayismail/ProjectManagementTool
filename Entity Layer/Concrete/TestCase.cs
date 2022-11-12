using System.ComponentModel.DataAnnotations;

namespace Entity_Layer.Concrete
{
    public class TestCase
    {
        [Key]
        public int TestCaseId { get; set; }
        public string? Name { get; set; }
        public string? Precondition { get; set; }
        public bool CaseResult { get; set; }
        public List<Step>? Steps { get; set; }
        public int TicketId { get; set; }
        public Ticket? Ticket { get; set; }
        public string? TestType { get; set; }
        public int TestSetId { get; set; }
        public TestSet TestSet { get; set; }
    }
}
