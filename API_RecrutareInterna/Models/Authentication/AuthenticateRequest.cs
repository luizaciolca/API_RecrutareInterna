using System.ComponentModel.DataAnnotations;

namespace API_RecrutareInterna.Models.Authentication
{
    public class AuthenticateRequest
    {
        [Required(ErrorMessage = "User Name is required")]
        public Guid Username { get; set; } //ID_Membru
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } //Nume


    }
}
