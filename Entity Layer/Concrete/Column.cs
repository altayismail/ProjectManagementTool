﻿using System.ComponentModel.DataAnnotations;

namespace Entity_Layer.Concrete
{
    public class Column
    {
        [Key]
        public int ColumnId { get; set; }
        public string? ColumnName { get; set; }
        public int ColumnOrder { get; set; }
        public List<Ticket>? Tickets { get; set; }
    }
}
