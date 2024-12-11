using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Model
{
    public class BookAuthor
    {
        public int BookId { get; private set; }
        public int AuthorId { get; private set; }
        public int Order {  get=>Author?.Order??0;  set { if (Author != null) Author.Order = value; }  }

        public Book Book { get;  set; }
        public Author Author { get;  set; }

    }
}
