using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_RecrutareInterna.Models
{
    public class EchipeItem
    {
        [Key]
        public Guid ID_Echipa {  get; set; }

        public string Departament { get; set; }

        public string Responsabilitati { get; set; }

        public DateTime Data_Infiintare { get; set; }


    }
} 
