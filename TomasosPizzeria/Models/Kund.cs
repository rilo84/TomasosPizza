﻿using System;
using System.Collections.Generic;

namespace TomasosPizzeria.Models
{
    public partial class Kund
    {
        public Kund()
        {
            Bestallning = new HashSet<Bestallning>();
        }

        public string KundId { get; set; }
        public string Namn { get; set; }
        public string Gatuadress { get; set; }
        public string Postnr { get; set; }
        public string Postort { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string AnvandarNamn { get; set; }
        public string Losenord { get; set; }

        public virtual ICollection<Bestallning> Bestallning { get; set; }
    }
}
