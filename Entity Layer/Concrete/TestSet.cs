using System.ComponentModel.DataAnnotations;

namespace Entity_Layer.Concrete
{
    public class TestSet
    {
        [Key]
        public int TestSetId { get; set; }
        public string? Name { get; set; }
        public string? TestSetResult { get; set; }
        public List<TestCase>? TestCases { get; set; }
    }
}
