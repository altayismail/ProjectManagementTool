using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity_Layer.Concrete
{
    public class Step
    {
        [Key]
        public int StepId { get; set; }
        public int StepOrder { get; set; }
        public string? Action { get; set; }
        public string? Data { get; set; }
        public string? ExceptedResult { get; set; }
        public int TestCaseId { get; set; }
        [NotMapped]
        public virtual int OldStepOrder { get; set; }
        public TestCase? TestCase { get; set; }
    }
}
