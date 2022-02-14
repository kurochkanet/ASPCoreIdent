using AutoMapper;
using Contracts.Business;
using Contracts.Repositories;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void CreateCategory(CategoryDTO createCategoryDto)
        {
            _repository.Create(createCategoryDto);
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _repository.GetAll();
        }
        public CategoryDTO CategoryByID(int id)
        {
            var category = _repository.GetById(id);
            return category;
        }

        public void EditCategory(CategoryDTO editCategoryDto)
        {
            _repository.Edit(editCategoryDto);
        }
        public void DeleteCategory(int id)
        {
            _repository.Delete(id);
        }
    }
}
