using API_RecrutareInterna.DataContext;
using API_RecrutareInterna.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Cryptography;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel;
using System.IdentityModel;
using System.Text;
using API_RecrutareInterna.Models.Authentication;
using API_RecrutareInterna.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace MariaCiolca_API2.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataRecrutareInternaContext _context;
        private readonly IConfiguration _configuration;
        public UserRepository(DataRecrutareInternaContext context, IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var user = await _context.Membrii.SingleOrDefaultAsync(x => x.ID_Membru == request.Username && x.Nume == request.Password); //citesc de pe contex


            if (user == null)
            { return null; }

            var token = await GenerateJwtToken(request);
            // SecurityAlgorithms.HmacSha256
            return new AuthenticateResponse(token);
        }

        private async Task<string> GenerateJwtToken(AuthenticateRequest request)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (_configuration.GetValue<string>("Authentication:Secret")));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                _configuration.GetValue<string>("Authentication:Domain"),
                _configuration.GetValue<string>("Authentication:Audience"),
               null,
               expires: DateTime.Now.AddHours(2),
               signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
