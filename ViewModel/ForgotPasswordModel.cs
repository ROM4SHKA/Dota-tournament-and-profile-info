using System.ComponentModel.DataAnnotations;
namespace KursachV2.ViewModel
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Поле 'Email' необхідне.")]
        [EmailAddress]
        public string Email { get; set;}
        public int Danger { get; set; }
        public int Gone { get; set; }
    }
}
