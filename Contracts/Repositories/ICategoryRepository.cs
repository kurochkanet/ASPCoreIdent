using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
   public interface ICategoryRepository
    {
        IEnumerable<CategoryDTO> GetAll();
        CategoryDTO GetById(int id);
        void Create(CategoryDTO createCategoryDto);
        void Edit(CategoryDTO editCategoryDto);
        void Delete(int id);
    }
}
