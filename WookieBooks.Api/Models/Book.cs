using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WookieBooks.Api.Models
{
    public class Book
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public byte[] CoverImage { get; set; }

        public decimal Price { get; set; }

    }
}
