using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Cresk.ViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
