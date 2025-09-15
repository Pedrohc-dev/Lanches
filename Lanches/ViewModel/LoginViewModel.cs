using System.ComponentModel.DataAnnotations;

namespace Lanches.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name ="Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } //Objetivo retornar o usuario a pagina que ele queria acessar antes da autheticacao
    }
}
