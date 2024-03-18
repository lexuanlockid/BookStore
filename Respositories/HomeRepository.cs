using BookStore.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Respositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _context;
        public HomeRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Genre>> Genres()
        {
            return await _context.Genres.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProducts(string sTerm="", int genreId=0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Product> products = await (from product in _context.Products
                            join genre in _context.Genres
                            on product.GenreId equals genre.Id
                            where string.IsNullOrWhiteSpace(sTerm) || (product!=null && product.Title.ToLower().StartsWith(sTerm)) 
                            select new Product
                            {
                                Id = product.Id,
                                Title = product.Title,
                                GenreId = product.GenreId,
                                Price = product.Price,
                                ImgUrl = product.ImgUrl
                            }).ToListAsync();
            if (genreId > 0)
            {
                products = products.Where(o => o.GenreId == genreId).ToList();
            }
            return products;
        }
    }
}
