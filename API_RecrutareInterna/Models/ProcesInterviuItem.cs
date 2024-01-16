using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_RecrutareInterna.Models
{
    public class ProcesInterviuItem
    {
        [Key]
        public Guid ID_Proces_interviu { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        public string Etape { get; set; }

        [ForeignKey("Proiecte")]
        public Guid ID_Proiecte { get; set; }

        [ForeignKey("Echipe")]
        public Guid ID_Echipa { get; set; }

    }
}
