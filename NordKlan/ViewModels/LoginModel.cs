using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NordKlan.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Login not specified")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
