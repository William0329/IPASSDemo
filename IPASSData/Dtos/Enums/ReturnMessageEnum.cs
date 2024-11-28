using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IPASSData.Dtos.Enums
{
    /// <summary>
    /// 回應訊息
    /// </summary>
    public enum ReturnMessageEnum
    {
        /// <summary>
        /// 帳號重複訊息
        /// </summary>
        [Display(Name = "帳號已存在")]
        AccountExist,
        /// <summary>
        /// 無帳號訊息
        /// </summary>
        [Display(Name = "帳號不存在")]
        AccountNotExist,
        /// <summary>
        /// 登入錯誤訊息
        /// </summary>
        [Display(Name ="登入帳號密碼錯誤或不存在")]
        UserLoginError,
        /// <summary>
        /// 登出錯誤訊息
        /// </summary>
        [Display(Name = "登出失敗，請檢查UserId或Token是否正確填寫")]
        UserLogoutError,
        /// <summary>
        /// 登出訊息
        /// </summary>
        [Display(Name = "登出成功")]
        UserLogoutSuccess,
        /// <summary>
        /// 登入驗證過程失敗
        /// </summary>
        FidoLoginError,
        /// <summary>
        /// 資料容已存在
        /// </summary>
        [Display(Name = "資料已存在")]
        DataExist,
    }
}
