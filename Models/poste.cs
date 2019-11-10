using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wafabank.Models
{
    public class poste
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public virtual ICollection<offre> jobs { get; set; }

    }
}