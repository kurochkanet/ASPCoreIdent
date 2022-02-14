using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class GoodDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Descr { get; set; }
        public string Photo { get; set; }
        public string PhotoFileName { get; set; }
        public bool Reserve { get; set; }
        public int Qty { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
