using System;
using System.ComponentModel.DataAnnotations;

namespace KursachV2.ViewModel
{
    public class ChangePasswordModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage ="Поле 'Новий пароль' необхідне.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Пароль має мати не менше 4 символів", MinimumLength = 4)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Поле 'Старий пароль' необхідне.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set;}
        public int Danger { get; set; }
        public int Gone { get; set; }
    }
}
