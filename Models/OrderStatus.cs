using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        [Required]
        [MaxLength(20)]
        public string? Status { get; set; }
    }
}
