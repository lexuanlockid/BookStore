using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    [Table("Genre")]
    
    public class Genre
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]  
        public string? GenreName { get; set; }
        public List<Product> Products { get; set; } 
    }
}
