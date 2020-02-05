using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeria.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("Namn:")]
        public string Namn { get; set; }

        [Required]
        [DisplayName("Gatuadress:")]
        public string Gatuadress { get; set; }

        [Required]
        [DisplayName("Postnummer:")]
        public string Postnr { get; set; }

        [Required]
        [DisplayName("Postort:")]
        public string Postort { get; set; }

        [Required]
        [DisplayName("Email:")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Telefon:")]
        public string Telefon { get; set; }

        [Required]
        [DisplayName("Användarnamn:")]
        public string AnvandarNamn { get; set; }

        [Required]
        [DisplayName("Lösenord:")]
        public string Losenord { get; set; }
    }
}
