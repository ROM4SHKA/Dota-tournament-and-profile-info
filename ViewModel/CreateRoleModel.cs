using System.ComponentModel.DataAnnotations;

namespace KursachV2.ViewModel
{
    public class CreateRoleModel 
    {
        [Required(ErrorMessage = "Поле 'Назва ролі' необхідне")]

        public string Name { get; set; }
        public int Gone { get; set; }
        public int Danger { get; set; }
    }
}
