using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace wafabank.Models
{
    public class offre
    {
        public int Id { get; set; }
        public virtual Secteur Secteur { get; set; }
        public  virtual  poste poste { get; set; }
        public virtual  Niveau Niveau { get; set; }
        public virtual  ville ville { get; set; }
        public string Adresse { get; set; }
        public string Description { get; set; }
        [Required]
        public string photo { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime DatePost { get; set; }
        public int posteId { get; set; }
        public int villeId { get; set; }
        public int NiveauId { get; set; }
        public int SecteurId { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}