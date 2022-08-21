using BlogApp.Backend.Entities;
using BlogApp.Backend.Interface;
using BlogApp.Common.Model.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApp.Backend.BusinessLayer
{
    public class AuthenticationBL : IAuthenticationBL
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthenticationBL(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }

        public (JwtSecurityToken, string) Authenticate(User loginUser)
        {
            if (loginUser != null && loginUser.Username != null && loginUser.Password != null)
            {
                User user = _userRepository.GetByUsername(loginUser.Username);
                if (ValidateUser(user, loginUser.Password))
                {
                    Claim[] claims = GenerateClaims(user);
                    JwtSecurityToken token = GenerateToken(claims);
                    return (token, string.Empty);
                }
                return (null, "Invalid credentials");
            }
            return (null, null);
        }

        private Claim[] GenerateClaims(User user)
        {
            return new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(Constants.UserId, user.Id.ToString()),
                new Claim(Constants.UserName, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
        }

        private JwtSecurityToken GenerateToken(Claim[] claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);
        }

        private bool ValidateUser(User user, string password)
        {
            if (user != null)
            {
                if (user.Password == Convert.ToBase64String(Encoding.ASCII.GetBytes(password)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
