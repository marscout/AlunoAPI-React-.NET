using System.ComponentModel.DataAnnotations;

namespace AlunoApi.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Email é obrigatório")]
        [EmailAddress(ErrorMessage ="Formato de email inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatório")]
        [StringLength(20, ErrorMessage ="A {0} deve ter no mínimo {2} e no máximo {1} caracteres.",MinimumLength =10)]
        public string Password { get; set; }
    }
}
