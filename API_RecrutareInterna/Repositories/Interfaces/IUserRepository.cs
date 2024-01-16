using API_RecrutareInterna.Models.Authentication;

namespace API_RecrutareInterna.Repositories.Interfaces
{
  
    public interface IUserRepository
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
    }
}

