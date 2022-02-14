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
    public class GoodsRepository : IGoodsRepository
    {
        private DataContext _context;
        IMapper _mapper;
        public GoodsRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<GoodDTO> GetAll()
        {
            var dbGoods = _context.Goods.Include(el => el.Category).ToArray();
            IEnumerable<GoodDTO> result = _mapper.Map<IEnumerable<GoodDTO>>(dbGoods);
            return result;
        }
        public void Create(GoodDTO createGoodDto)
        {
            Good good = _mapper.Map<Good>(createGoodDto);

            var category = this._context.Categories.FirstOrDefault(el => el.Id == createGoodDto.CategoryId);
            good.Category = category;

            _context.Goods.Add(good);
            _context.SaveChanges();
        }
        public void Edit(GoodDTO editGoodDto)
        {
            Good editgood = _mapper.Map<Good>(editGoodDto);
            _context.Entry(editgood).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Good good = _context.Goods.Find(id);
            if (good != null)
            {
                _context.Goods.Remove(good);
                _context.SaveChanges();
            }
        }

        public IEnumerable<GoodDTO> GoodByID(int id)
        {
            var dbGoods = _context.Goods.Include(el => el.Category).ToArray();
            IEnumerable<GoodDTO> result = _mapper.Map<IEnumerable<GoodDTO>>(dbGoods);
            return result;
        }

        public GoodDTO GetById(int id)
        {
            var dbGood = _context.Goods.SingleOrDefault(el => el.Id == id);
            GoodDTO result = _mapper.Map<GoodDTO>(dbGood);
            return result;
        }

    }
}
