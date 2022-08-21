using BlogApp.Backend.Interface;
using BlogApp.Common.Model.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BlogApp.Backend.Controllers
{    
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationBL _authenticationBL;

        public AuthenticationController(IAuthenticationBL authenticationBL)
        {
            _authenticationBL = authenticationBL;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Post([FromBody] User user)
        {
            (JwtSecurityToken token, string message) = _authenticationBL.Authenticate(user);
            if (token != null)
            {
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            if (message != null)
            {
                return BadRequest(message);
            }
            return BadRequest();
        }
    }
}