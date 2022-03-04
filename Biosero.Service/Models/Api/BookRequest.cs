namespace Biosero.Service.Models.Api
{
    public class BookRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string CoverImage { get; set; }

        public decimal Price { get; set; }

        public bool IsPublished { get; set; }
    }
}
