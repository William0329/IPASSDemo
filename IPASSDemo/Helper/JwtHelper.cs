using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IPASSDemo.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration configuration;

        public JwtHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(string userName, List<string> systems = null, string userId = null, string companyId = null)
        {
            string? _issuer;
            byte[] _signKey = Array.Empty<byte>();
            var expireMinutes = configuration.GetValue<int>($"JwtSettings:ExpireMinutes");
            _issuer = configuration.GetValue<string>($"JwtSettings:Issuer");
            if (!string.IsNullOrEmpty(_issuer))
            {
                _signKey = Convert.FromBase64String(configuration.GetValue<string>($"JwtSettings:Key")!);
            }

            // Configuring "Claims" to your JWT Token
            List<Claim> _claims = new();
            _claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, userName));
            _claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // JWT ID
            if (systems != null)
            {
                _claims.Add(new Claim("Systems", JsonConvert.SerializeObject(systems)));
            }
            if (userId != null)
            {
                _claims.Add(new Claim("UserId", userId));
            }
            if (companyId != null)
            {
                _claims.Add(new Claim("CompanyId", companyId));
            }
            //_claims.Add(new Claim("uuid", uuid)); // api gateway傳來的uuid

            // The "NameId" claim is usually unnecessary.
            //claims.Add(new Claim(JwtRegisteredClaimNames.NameId, userName));

            // This Claim can be replaced by JwtRegisteredClaimNames.Sub, so it's redundant.
            //claims.Add(new Claim(ClaimTypes.Name, userName));

            // TODO: You can define your "roles" to your Claims.
            //claims.Add(new Claim("roles", "Admin"));
            //claims.Add(new Claim("roles", "Users"));

            var _userClaimsIdentity = new ClaimsIdentity(_claims);

            // Create a SymmetricSecurityKey for JWT Token signatures
            var _securityKey = new SymmetricSecurityKey(_signKey);

            // HmacSha256 MUST be larger than 128 bits, so the key can't be too short. At least 16 and more characters.
            // https://stackoverflow.com/questions/47279947/idx10603-the-algorithm-hs256-requires-the-securitykey-keysize-to-be-greater
            var _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature);

            // Create SecurityTokenDescriptor
            var _tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _issuer,
                //Audience = issuer, // Sometimes you don't have to define Audience.
                //NotBefore = DateTime.Now, // Default is DateTime.Now
                //IssuedAt = DateTime.Now, // Default is DateTime.Now
                Subject = _userClaimsIdentity,
                Expires = DateTime.Now.AddMinutes(expireMinutes),
                SigningCredentials = _signingCredentials
            };

            // Generate a JWT securityToken, than get the serialized Token result (string)
            var _tokenHandler = new JwtSecurityTokenHandler();
            var _securityToken = _tokenHandler.CreateToken(_tokenDescriptor);
            var _serializeToken = _tokenHandler.WriteToken(_securityToken);

            return _serializeToken;
        }
    }
}
