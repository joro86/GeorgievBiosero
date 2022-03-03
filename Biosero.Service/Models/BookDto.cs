﻿namespace Biosero.Service.Models
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public AuthorDto Author { get; set; }

        public string CoverImage { get; set; }

        public decimal Price { get; set; }

    }
}
