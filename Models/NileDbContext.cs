using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nile.Models
{
    public class NileDbContext : DbContext
    {
        public NileDbContext (DbContextOptions<NileDbContext> options) : base (options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
    }
}
