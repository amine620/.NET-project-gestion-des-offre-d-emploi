using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wafabank.Models
{
    public class contact
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "S'il vous plaît entrer l'adresse e-mail correcte")]
        public string Email { get; set; }
        [Required]
        public string subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}