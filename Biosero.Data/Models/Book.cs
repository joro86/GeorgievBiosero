using System;
using System.Collections.Generic;
using System.Text;

namespace Biosero.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Author Author { get; set; }

        public string CoverImage { get; set; }

        public decimal Price { get; set; }
    }
}
