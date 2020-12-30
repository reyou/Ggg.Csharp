using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace intro1
{
    public class ExampleModel
    {
        [Required] 
        [StringLength(10, ErrorMessage = "Name is too long.")] 
        public string Name { get; set; }
    }
}
