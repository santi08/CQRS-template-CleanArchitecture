using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string AuthorId { get; set; }

        public AppUser Author { get; set; }
    }
}
