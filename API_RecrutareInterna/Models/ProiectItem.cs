using API_RecrutareInterna.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_RecrutateInterna.Models
{
    public class ProiectItem
    {
        [Key]
        public Guid ID_Proiecte { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        public string Titlu { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        public string Descriere { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        [ForeignKey("Echipe")]
        public Guid ID_Echipa { get; set; }

    }
}
