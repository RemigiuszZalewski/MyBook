namespace MyBookAPI.Application.Books.Models
{
    public class BookDto
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public bool ToBeSold { get; set; }
        public int Pages { get; set; }
        public string Category { get; set; }
        public string PublishingHouse { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
}
