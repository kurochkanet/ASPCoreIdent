using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IGoodsRepository
    {
        IEnumerable<GoodDTO> GetAll();
        void Create(GoodDTO createGoodDto);
        void Edit(GoodDTO editGoodDto);
        void Delete(int id);
        GoodDTO GetById(int id);
    }
}
