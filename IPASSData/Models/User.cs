using IPASSData.Models.Shared;

using System.ComponentModel.DataAnnotations;

namespace IPASSData.Models
{
    public class User : DbBaseModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(50)]
        public string? Username { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        [MaxLength(50)]
        public string? Ac { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [MaxLength(255)]
        public string? Sw { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        [MaxLength(255)]
        public string? Email { get; set; }

        /// <summary>
        /// 手機
        /// </summary>
        [MaxLength(50)]
        public string? Phone { get; set; }

        /// <summary>
        /// 驗證器 User Id
        /// </summary>
        [MaxLength(50)]
        public string? AuthenticatorUserId { get; set; }
        /// <summary>
        /// 平台來源Id
        /// </summary>
        public string? SourceId { get; set; }

        /// <summary>
        /// 超級使用者
        /// </summary>
        public bool Is_SuperUser { get; set; }

        /// <summary>
        /// 帳號狀態
        /// </summary>
        public bool Is_Active { get; set; }

        /// <summary>
        /// 上次登入時間
        /// </summary>
        public DateTime? Last_Login { get; set; }
    }
}
