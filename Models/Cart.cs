using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nile.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public void AddItem(Book bk, int qty)
        {
            CartLine line = Lines
                .Where(b => b.Book.BookId == bk.BookId)
                .FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Book = bk,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }

        // part 2 BuildCartModel
        public void RemoveLine(Book bk) =>
            Lines.RemoveAll(x => x.Book.BookId == bk.BookId);

        // Empty (clear) the cart
        public void Clear() => Lines.Clear();

        // calculate the cart total
        public double ComputeTotalSum()
        {
            return (Lines.Sum(e => e.Book.Price * e.Quantity));
        }

        public class CartLine
        {
            public int CartLineID { get; set; }
            public Book Book { get; set; }
            public int Quantity { get; set; }
        }



    }
}
