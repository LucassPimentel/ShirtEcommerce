using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace sh_rt.ViewModels
{
    public abstract class UserViewModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Informe o e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
