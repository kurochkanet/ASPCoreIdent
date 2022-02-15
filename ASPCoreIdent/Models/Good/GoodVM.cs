using BLL.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreIdent.Models.Good
{
    public class GoodVM
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }

        public string Descr { get; set; }

        private string _photo;
        public string PhotoFileName
        {
            get
            {
                var result = $"{ContentPathBuilder.ContentGoodsFolder}/{_photo}";
                return result;
            }
        }
        public string Photo
        {
            get
            {
                return _photo;
            }
            set { _photo = value; }
        }

        public string PhotoThumbFileName
        {
            get
            {
                var result = $"{ContentPathBuilder.ContentThumbnailsFolder}/{_photo}";
                return result;
            }
        }

        public bool Reserve { get; set; }
        [Required]
        public int Qty { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

      //  public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
