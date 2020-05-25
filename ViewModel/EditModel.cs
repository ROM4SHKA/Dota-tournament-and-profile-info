using System.ComponentModel.DataAnnotations;

namespace KursachV2.ViewModel
{
    public class EditModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Поле 'Email' необхідне.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле 'Steam32 ID' необхідне.")]
        [Display(Name = "Steam32 ID")]
        public int? Account_id { get; set; }
        public string OldEmail { get; set; }
        public int? OldAccount_id { get; set; }
        public int Danger1 { get; set; }
        public int Danger { get; set; }
        public int Gone { get; set; }
    }
}
