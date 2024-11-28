using System.ComponentModel.DataAnnotations;

namespace IPASSData.Dtos.Enums
{
    /// <summary>
    /// User 狀態
    /// </summary>
    public enum UserStatusEnum
    {
        /// <summary>
        /// 停用
        /// </summary>
        [Display(Name = "停用")]
        停用 = 0,
        /// <summary>
        /// 啟用
        /// </summary>
        [Display(Name = "啟用")]
        啟用 = 1,
    }
}
