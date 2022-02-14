using AutoMapper;
using Contracts.Repositories;
using DAL.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;
        IMapper _mapper;
        public CategoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Create(CategoryDTO createCategoryDto)
        {
            Category category = _mapper.Map<Category>(createCategoryDto);

            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Edit(CategoryDTO editCategoryDto)
        {
            Category editcategory = _mapper.Map<Category>(editCategoryDto);
            _context.Entry(editcategory).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Category category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            var dbCategories = _context.Categories.ToArray();
            IEnumerable<CategoryDTO> result = _mapper.Map<IEnumerable<CategoryDTO>>(dbCategories);
            return result;
        }

        public CategoryDTO GetById(int id)
        {
            var dbCategory = _context.Categories.SingleOrDefault(el => el.Id == id);
            CategoryDTO result = _mapper.Map<CategoryDTO>(dbCategory);
            return result;
        }
        public IEnumerable<CategoryDTO> CategoryByID(int id)
        {
            var dbCategory = _context.Categories.SingleOrDefault(el => el.Id == id);
            IEnumerable<CategoryDTO> result = _mapper.Map<IEnumerable<CategoryDTO>>(dbCategory);
            return result;
        }
    }
}
