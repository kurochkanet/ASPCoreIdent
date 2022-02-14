using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Business
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAll();
        CategoryDTO CategoryByID(int id);
        void CreateCategory(CategoryDTO createCategoryDto);
        void EditCategory(CategoryDTO editCategoryDto);
        void DeleteCategory(int id);
    }
}
