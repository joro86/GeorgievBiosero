namespace Biosero.Service.Models.Api
{
    public class BookSearchRequest
    {
        public string Query { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Authour { get; set; }
    }
}
