﻿namespace Biosero.Service.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        //TODO: Maybe use a separate class here
        public string Author { get; set; }

        public string CoverImage { get; set; }

        public decimal Price { get; set; }

    }
}
