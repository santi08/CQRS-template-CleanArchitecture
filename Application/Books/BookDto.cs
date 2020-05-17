using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Books
{
    public class BookDto
    {
        public Guid BookId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
    }
}
