using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.ViewModels
{
    public class AccountViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Användarnamn är obligatoriskt")]
        [StringLength(20, ErrorMessage = "Användarnamn får vara max 20 tecken")]
        [DisplayName("Användarnamn:")]
        public string AnvandarNamn { get; set; }

        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
        [RegularExpression(@"(?=^.{6,20}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$", ErrorMessage = "Lösenordet är för enkelt")]
        [StringLength(20, ErrorMessage = "Lösenord måste vara mellan 6-20 tecken", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("Lösenord:")]
        public string Losenord { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"(?=^.{6,20}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$", ErrorMessage = "Lösenordet är för enkelt")]
        [StringLength(20, ErrorMessage = "Lösenord måste vara mellan 6-20 tecken", MinimumLength = 6)]
        [DisplayName("Nytt lösenord:")]
        public string NewPassword { get; set; }




    }
}
