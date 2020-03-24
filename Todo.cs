using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tasks.Data.Entity
{
    public class Todo { 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    

        public int Id { get; set; }
    public string Event { get; set; }
    public string Notes { get; set; }
        public DateTime DateTime { get; set; } = DateTime.MinValue;
    
    }
}
