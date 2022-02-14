using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class DataContext : DbContext
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
           // Database.EnsureCreated();
        }
    }
}
