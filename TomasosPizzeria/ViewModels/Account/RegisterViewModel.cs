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
        [Required(ErrorMessage = "Namn är obligatoriskt")]
        [StringLength(100, ErrorMessage = "Namn får vara max 100 tecken")]
        [DisplayName("Namn:")]
        
        public string Namn { get; set; }

        [Required(ErrorMessage = "Gatuadress är obligatoriskt")]
        [StringLength(50, ErrorMessage = "Gatuadress får vara max 50 tecken")]
        [DisplayName("Gatuadress:")]
        public string Gatuadress { get; set; }

        [Required(ErrorMessage = "Postnummer är obligatoriskt")]
        [StringLength(20, ErrorMessage = "Postnummer får vara max 20 tecken")]
        [DisplayName("Postnummer:")]
        public string Postnr { get; set; }

        [Required(ErrorMessage = "Postort är obligatoriskt")]
        [StringLength(100, ErrorMessage = "Postort får vara max 100 tecken")]
        [DisplayName("Postort:")]
        public string Postort { get; set; }

        [Required(ErrorMessage = "Email är obligatoriskt")]
        [StringLength(50, ErrorMessage = "Email får vara max 50 tecken")]
        [DisplayName("Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefonummer är obligatoriskt")]
        [StringLength(50, ErrorMessage = "Telefonummer får vara max 50 tecken")]
        [DisplayName("Telefon:")]
        public string Telefon { get; set; }

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
