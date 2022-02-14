using AutoMapper;
using DAL.Models;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class DalMapper : Profile
    {
        public DalMapper()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Good, GoodDTO>()
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name)
                ).ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
