using System.ComponentModel.DataAnnotations;

namespace KursachV2.ViewModel
{
    public class CreateModel
    {
        [Required(ErrorMessage = "Поле 'Email' необхідне.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле 'Steam32 ID' необхідне.")]
        [Display(Name = "Steam32 ID")]
        public int Account_id { get; set; }

        [Required(ErrorMessage = "Поле 'Пароль' необхідне.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Пароль має мати не менше 4 символів", MinimumLength = 4)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Поле 'Підтвердження паролю' необхідне.")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження паролю")]
        public string ConfirmPassword { get; set; }
        public int Danger { get; set; }
        public int Gone { get; set; }
    }
}
