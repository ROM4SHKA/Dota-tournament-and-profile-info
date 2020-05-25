using System.ComponentModel.DataAnnotations;

namespace KursachV2.ViewModel
{
    public class ResetPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле 'Новий пароль' необхідне.")]
        [StringLength(100, ErrorMessage = "Мінімальна довжина паролю 4 символи", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Поле 'Підтвердження паролю' необхідне.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
        public int Danger { get; set; }
        public int Gone { get; set; }
    }
}
