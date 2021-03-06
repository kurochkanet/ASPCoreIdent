using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Good
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Descr { get; set; }
        [MaxLength(2096)]
        public string Photo { get; set; }
        public bool Reserve { get; set; }
        public int Qty { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
