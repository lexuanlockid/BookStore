using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    [Table("Product")]
    public class Product
    {
        public int Id {  get; set; }

        [Required]
        [MaxLength(40)]
        public string? Title { get; set; }
        [Range(0, 10000)]
        [Required]
        public int Price { get; set; }
        [Required]
        public int GenreId { get; set; }
        public string? ImgUrl { get; set; }  
        public Genre Genre { get; set; }    
        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetails { get; set; }
    }
}
