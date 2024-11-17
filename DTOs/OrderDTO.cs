using System.ComponentModel.DataAnnotations;

namespace sh_rt.DTOs
{
    public class OrderDTO
    {
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
        public string? Number { get; set; }

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
    }
}
