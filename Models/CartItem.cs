using System.ComponentModel.DataAnnotations;

namespace sh_rt.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public Shirt? Shirt { get; set; }
        public int Quantity { get; set; }

        [StringLength(200)]
        public string? CartId { get; set; }
    }
}
