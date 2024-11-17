using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sh_rt.Models
{
    public class Shirt
    {
        // TODO: Inserir imagens no banco de dados

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira um nome.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "O {0} deve ter no mínimo 1 caractere e no máximo 50.")]
        public string? Name { get; set; }

        [MaxLength(100)]
        [StringLength(100, ErrorMessage = "O {0} pode ter no máximo 100 caracteres.")]
        public string? ShortDescription { get; set; }

        [MaxLength(300)]
        [StringLength(300, ErrorMessage = "O {0} pode ter no máximo 300 caracteres.")]
        public string? LongDescription { get; set; }

        [Required(ErrorMessage = "Insira um preço.")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999, ErrorMessage = "O preço deve estar entre 1 e 999 reais.")]
        public decimal Price { get; set; }

        public string? FrontShirtImageUrl { get; set; }

        public string? BehindShirtImageUrl{ get; set; }

        public string? ImageThumbnailUrl { get; set; }
        public bool? IsFavoriteShirt { get; set; }
        public bool? InStock { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
