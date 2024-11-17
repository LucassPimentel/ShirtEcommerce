using System.ComponentModel.DataAnnotations;

namespace sh_rt.ViewModels
{
    public class RegisterViewModel : UserViewModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Informe o nome completo")]
        [Display(Name = "Nome Completo")]
        public string Name { get; set; }
    }
}
