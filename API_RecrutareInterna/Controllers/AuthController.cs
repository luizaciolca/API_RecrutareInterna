/*
using API_RecrutareInterna.Models.Authentication;
using API_RecrutareInterna.Repositories.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace API_RecrutareInterna.Controllers
{

    /*   [Route("api/[controller]")]
       [ApiController]
       public class AuthController : ControllerBase
       {
          
           public AuthController(IUserService userService)
           {
               _userService = userService;
           }

           [HttpPost("authenticate")]
           public IActionResult Authenticate(AuthenticateRequest model)
           {
               var response = _userService.Authenticate(model);

               if (response == null)
               {
                   return BadRequest(new { message = "Username sau parola incorecte" });
               }

               return Ok(response);
               }
           } */

/*
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
        {
        private IUserRepository _userService;
        private readonly ILogger<AuthController> _logger;

            public AuthController(IUserRepository userRepository, ILogger<AuthController> logger)
            {
                _userService = userRepository;
                _logger = logger;
            }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            var response = await _userService.Authenticate(request);
            if (response == null)
            {
                _logger.LogWarning("Cineva cu user sau parole gresite vrea sa se logheze");
                return StatusCode((int)HttpStatusCode.BadRequest, "Username sau parola gresite");
            }

            return Ok(response);
        }

    }
} */


using API_RecrutareInterna.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API_RecrutareInterna.Repositories.Interfaces;
using System.Net;

namespace JWTRefreshToken.NET6._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserRepository userRepository, ILogger<AuthController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            var response = await _userRepository.Authenticate(request);
            if (response == null)
            {
                _logger.LogWarning("Cineva cu user sau parole gresite vrea sa se logheze");
                return StatusCode((int)HttpStatusCode.BadRequest, "Username sau parola gresite");
            }

            return Ok(response);
        }
    }
}