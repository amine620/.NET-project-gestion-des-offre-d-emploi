using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wafabank.Models
{
    public class Demande
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "votre Champ est vide")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "votre Champ est vide")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "votre Champ est vide")]
        public string CIN { get; set; }

        [Required(ErrorMessage = "votre Champ est vide")]
        public string Adresse { get; set; }
        [Required(ErrorMessage = "votre Champ est vide")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,10}$", ErrorMessage = "S'il vous plaît entrer Le Telephone correcte")]
        public string Tele { get; set; }

        [EmailAddress(ErrorMessage = "S'il vous plaît entrer l'adresse e-mail correcte")]
        public string Email { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "saisi votre Champ")]
        public string Message { get; set; }

        [Required(ErrorMessage = "saisi votre Champ")]
        public string cv { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int jobId { get; set; }
        public virtual offre job { get; set; }
    }
}
