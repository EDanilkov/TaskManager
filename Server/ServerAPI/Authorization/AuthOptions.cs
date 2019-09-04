using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ServerAPI.Authorization
{
    public class AuthOptions
    {
        public const string ISSUER = "ServerAPI"; // издатель токена
        public const string AUDIENCE = "http://localhost:44316/"; // потребитель токена
        const string KEY = "secretkeyforServerAPI"; // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
