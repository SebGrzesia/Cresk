using System.ComponentModel.DataAnnotations;

namespace Cresk.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }

    }
}
