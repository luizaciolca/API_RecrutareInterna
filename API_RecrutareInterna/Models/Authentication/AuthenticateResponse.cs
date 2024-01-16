using API_RecrutareInterna.Models;
using Humanizer;

namespace API_RecrutareInterna.Models.Authentication
{
    public class AuthenticateResponse
{
         public Guid ID_Membru { get; set; }
         public string Nume { get; set; }
         public string Functie { get; set; }
         public string Token { get; set; }

         public AuthenticateResponse(MembriiItem user, string token)
         {
             ID_Membru = user.ID_Membru;
             Nume = user.Nume;
             Functie = user.Functie;
             Token = token;
         }

         public AuthenticateResponse(string token)
         {
             Token = token;
         }
     } 
}

