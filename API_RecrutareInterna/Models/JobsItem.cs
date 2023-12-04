using System.ComponentModel.DataAnnotations;

namespace API_RecrutateInterna.Models
{
    public class JobsItem
    {
        [Key]
        public Guid ID_job { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        public DateTime ValidTo { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        public DateTime ValidFrom { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        public string? Pozitie { get; set; }

    }
}
