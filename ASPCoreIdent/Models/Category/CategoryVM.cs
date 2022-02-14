using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreIdent.Models.Category
{
    public class CategoryVM
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
    }
}
