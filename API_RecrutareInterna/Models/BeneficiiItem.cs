using System;
using System.ComponentModel.DataAnnotations;

namespace API_RecrutateInterna.Models
{
    public class BeneficiiItem
    {
        [Key]
        public Guid ID_Beneficii { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        public string Titlu { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        public string Descriere { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        public string Departament { get; set; }
    }
}


