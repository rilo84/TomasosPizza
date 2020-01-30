using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TomasosPizzeria.Models
{
    public partial class Kund
    {
        public Kund()
        {
            Bestallning = new HashSet<Bestallning>();
        }

        public int KundId { get; set; }
        [DisplayName("Namn:")]
        public string Namn { get; set; }

        [DisplayName("Gatuadress:")]
        public string Gatuadress { get; set; }

        [DisplayName("Postnummer:")]
        public string Postnr { get; set; }

        [DisplayName("Postort:")]
        public string Postort { get; set; }

        [DisplayName("Email:")]
        public string Email { get; set; }

        [DisplayName("Telefon:")]
        public string Telefon { get; set; }

        [DisplayName("Användarnamn:")]
        public string AnvandarNamn { get; set; }

        [DisplayName("Lösenord:")]
        public string Losenord { get; set; }

        public virtual ICollection<Bestallning> Bestallning { get; set; }
    }
}
