using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Extensions
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
    }
}
