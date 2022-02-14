using ASPCoreIdent.Models.Category;
using ASPCoreIdent.Models.Good;
using AutoMapper;
using DAL.Models;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreIdent.Models
{
    public class ViewMapper : Profile
    {
        public ViewMapper()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<GoodVM, GoodDTO>().ReverseMap();
            CreateMap<CategoryVM, CategoryDTO>().ReverseMap();
            
        }
    }
}
