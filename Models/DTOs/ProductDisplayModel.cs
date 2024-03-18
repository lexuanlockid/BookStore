namespace BookStore.Models.DTOs
{
    public class ProductDisplayModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public int GenreId { get; set; } = 0;
        public string STerm { get; set; } = "";
    }
}
