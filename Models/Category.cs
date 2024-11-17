using System.ComponentModel.DataAnnotations;

namespace sh_rt.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "O tamanho máximo foi atingido")]
        [Required(ErrorMessage = "Insira um nome.")]
        public string? Name { get; set; }

        [StringLength(200, ErrorMessage = "O tamanho máximo foi atingido.")]
        public string? Description { get; set; }

        public List<Shirt>? Shirts { get; set; }
    }
}
