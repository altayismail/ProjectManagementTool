using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity_Layer.Concrete
{
    public class TestSet
    {
        [Key]
        public int TestSetId { get; set; }
        public string? Name { get; set; }
        public string? TestSetResult { get; set; }
        public string? TestType { get; set; }
        public List<TestCase>? TestCases { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        public int TestCaseId { get; set; }
    }
}
