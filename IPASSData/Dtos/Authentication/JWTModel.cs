using System.ComponentModel.DataAnnotations;

namespace IPASSData.Dtos.Authentication
{
    public class JWTModel
    {
        /// <summary>
        /// 使用者 Id
        /// </summary>
        [Required]
        public string UserId { get; set; }
        /// <summary>
        /// 權杖
        /// </summary>
        [Required]
        public string JwtToken { get; set; }
    }
}
