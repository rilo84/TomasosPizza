using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Användarnamn:")]
        public string AnvandarNamn { get; set; }

        [Required]
        [DisplayName("Lösenord:")]
        public string Losenord { get; set; }
    }
}
