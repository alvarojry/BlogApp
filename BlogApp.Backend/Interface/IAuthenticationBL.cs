using BlogApp.Common.Model.Security;
using System.IdentityModel.Tokens.Jwt;

namespace BlogApp.Backend.Interface
{
    public interface IAuthenticationBL
    {
        (JwtSecurityToken, string) Authenticate(User loginUser);
    }
}
