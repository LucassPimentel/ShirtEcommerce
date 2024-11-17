using System.ComponentModel.DataAnnotations.Schema;

namespace sh_rt.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int ShirtId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public virtual Shirt Shirt { get; set; }
        public virtual Order Order { get; set; }
    }
}