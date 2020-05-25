using System.ComponentModel.DataAnnotations;
namespace KursachV2.ViewModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Поле 'Email' необхідне.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле 'Пароль' необхідне.")]
        [DataType(DataType.Password)]
        [Display(Name ="Пароль")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public int Danger { get; set; }
        public int Gone  { get; set; }
    }
}
