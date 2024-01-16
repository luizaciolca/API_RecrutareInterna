using System.ComponentModel.DataAnnotations;

namespace API_RecrutareInterna.Models
{
    public class MembriiItem
    {
        [Key]
        public Guid ID_Membru { get; set; }
        public string Nume { get; set; }
        public string Functie { get; set; }
        public string Parola { get; set;}
    }
}
