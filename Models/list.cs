using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wafabank.Models
{
    public class list
    {
        public offre job { get; set; }



        public ICollection<Secteur> Secteurs { get; set; }

        public ICollection<poste> postes { get; set; }
        public ICollection<Niveau> Niveaus { get; set; }
        public ICollection<ville> villes { get; set; }



    }
}