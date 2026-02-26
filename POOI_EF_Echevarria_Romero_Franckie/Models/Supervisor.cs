using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POOI_EF_Echevarria_Romero_Franckie.Models
{
    public class Supervisor
    {
        public int idsup { get; set; }

        [Required]
        public string nomsup { get; set; }

        [Required]
        public string dirsup { get; set; }

        [Required]
        [EmailAddress]
        public string emailsup { get; set; }

        [Required]
        public int idpais { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8)]
        public string dnisup { get; set; }
    }
}