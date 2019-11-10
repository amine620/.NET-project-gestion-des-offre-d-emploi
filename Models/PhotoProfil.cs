using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wafabank.Models
{
    public class PhotoProfil
    {
        public int Id { get; set; }
        public string photo { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}