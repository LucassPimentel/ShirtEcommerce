using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace sh_rt.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(60)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe o sobrenome")]
        [StringLength(120)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        [StringLength(100)]
        public string Address { get; set; }

        [MaxLength(1000)]
        public int? Number { get; set; }

        [Required(ErrorMessage = "Informe o estado")]
        [StringLength(50)]
        public string State { get; set; }

        [Required(ErrorMessage = "Informe a cidade")]
        [StringLength(100)]
        public string City { get; set; }

        [Required(ErrorMessage = "Informe o CEP")]
        [StringLength(10, MinimumLength = 8)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Informe o número de celular")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Informe seu melhor e-mail")]
        [StringLength(65)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalOrder { get; set; }

        [ScaffoldColumn(false)]
        public int TotalOrderItems { get; set; }

        public DateTime DispatchedAt { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public List<OrderDetail> OrderItems { get; set; }
    }
}
