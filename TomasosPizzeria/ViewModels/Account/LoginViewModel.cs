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
        [Required(ErrorMessage = "Användarnamn är obligatoriskt")]
        [StringLength(20, ErrorMessage = "Användarnamn får vara max 20 tecken")]
        [DisplayName("Användarnamn:")]
        public string AnvandarNamn { get; set; }

        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
        [StringLength(20, ErrorMessage = "Lösenord får vara max 20 tecken")]
        [DisplayName("Lösenord:")]
        public string Losenord { get; set; }
    }
}
