using System.ComponentModel.DataAnnotations;

namespace Entity_Layer.Concrete
{
    public class Step
    {
        [Key]
        public int StepId { get; set; }
        public string? Action { get; set; }
        public string? ExceptedResult { get; set; }
        public int TestCaseId { get; set; }
        public TestCase? TestCase { get; set; }
    }
}
