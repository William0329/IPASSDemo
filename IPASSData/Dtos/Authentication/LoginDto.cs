using IPASSData.CustomAttribute.Validations;
using System.ComponentModel.DataAnnotations;

namespace IPASSData.Dtos.Authentication
{
    /// <summary>
    /// 登入驗證 請求
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// 帳號
        /// </summary>
        [RequiredCheck]
        [MaxLength(50)]
        public string Ac { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [RequiredCheck]
        [MaxLength(50)]
        public string Sw { get; set; }
    }
    /// <summary>
    /// 登入驗證 回應
    /// </summary>
    public class LoginResponseDto
    {
        public LoginResponseDto()
        {
            Token = string.Empty;
            Id = string.Empty;
            Name = string.Empty;
            Email = string.Empty;
        }

        /// <summary>
        /// JWT
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}
