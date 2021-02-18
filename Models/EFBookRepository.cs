using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nile.Models
{
    public class EFBookRepository: IBookRepository
    {
        private NileDbContext _context;

        //Constructor
        public EFBookRepository (NileDbContext context)
        {
            _context = context;
        }

        public IQueryable<Book> Books => _context.Books;
    }
}
