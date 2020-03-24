using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tasks.Model
{
    public class TodoModel
    {
       [Required]
       [StringLength(1000,MinimumLength =1)]
        public string Event { get; set; }
        [Range(1,1000)]
        public string Notes { get; set; }
        [Required]
        public DateTime DateTime { get; set; } = DateTime.MinValue;
      

    }
}
