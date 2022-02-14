using DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Business
{
    public interface IGoodsService
    {
        IEnumerable<GoodDTO> GetAll();
        IEnumerable<GoodDTO> SearchByName(string name);
        GoodDTO GoodByID(int id);

        void CreateGood(GoodDTO createGoodDto, IFormFile formFile);
        void EditGood(GoodDTO editGoodDto, IFormFile formFile);
        void DeleteGood(int id);
    }
}
