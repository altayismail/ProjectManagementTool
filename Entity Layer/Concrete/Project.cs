using System.ComponentModel.DataAnnotations;

namespace Entity_Layer.Concrete
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public List<Ticket>? Tickets { get; set; }
    }
}
