using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DBInitalizer 
    {
        private DataContext _context;
        private ILogger<DBInitalizer> _logger;

        public DBInitalizer(
            DataContext context,
            ILogger<DBInitalizer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Seed()
        {
            _context.Database.Migrate();

            if (_context.Categories.Count() == 0)
            {
                var c1 = new Models.Category { Name = "Food" };
                var c2 = new Models.Category { Name = "Other" };
                _context.Categories.Add(c1);
                _context.Categories.Add(c2);
                _context.SaveChanges();
                _context.Goods.Add(new Models.Good
                {
                    CategoryId = c1.Id,
                    Name = "Cookie",
                    Price = 10,
                    Reserve = true
                });
                _context.Goods.Add(new Models.Good
                {
                    CategoryId = c2.Id,
                    Name = "Car",
                    Price = 10,
                });
                _context.Goods.Add(new Models.Good
                {
                    CategoryId = c1.Id,
                    Name = "TV",
                    Price = 10,
                    Reserve = false
                });

                _context.SaveChanges();

            }
        }
    }
}
