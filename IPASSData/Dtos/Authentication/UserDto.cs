using IPASSData.CustomAttribute.Validations;

using System.ComponentModel.DataAnnotations;

namespace IPASSData.Dtos.Authentication
{
    /// <summary>
    /// 帳號 資料
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Id
        /// </summary>
        [RequiredCheck]
        public string Id { get; set; }
        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string? Username { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        public string? Ac { get; set; }
        /// <summary>
        /// 權限群組名稱
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 帳號狀態
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 帳號創立者名稱
        /// </summary>
        public string CreateName { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public string UpdateTime { get; set; }
    }
    /// <summary>
    /// 新增 帳號 資料
    /// </summary>
    public class AddUserDto
    {
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [RequiredCheck]
        [MaxLength(50)]
        public string? Username { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        [RequiredCheck]
        [MaxLength(50)]
        public string? Ac { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [RequiredCheck, PasswordVerify]
        public string? Sw { get; set; }
    }

    /// <summary>
    /// 更新 帳號 資料
    /// </summary>
    public class UpdateUserDto
    {
        /// <summary>
        /// Id
        /// </summary>
        [RequiredCheck]
        public string Id { get; set; }
        /// <summary>
        /// 使用者名稱
        /// </summary>
        [RequiredCheck]
        [MaxLength(50)]
        public string? Username { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        [RequiredCheck]
        [MaxLength(50)]
        public string? Ac { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [PasswordVerify]
        public string? Sw { get; set; }
        /// <summary>
        /// 隸屬法人
        /// </summary>
        [RequiredCheck]
        public string LegalEntityId { get; set; }
        /// <summary>
        /// 帳號權限
        /// </summary>
        [RequiredCheck]
        public string GroupId { get; set; }
        /// <summary>
        /// 帳號狀態
        /// </summary>
        public bool Is_Active { get; set; }
    }
    /// <summary>
    /// 更新 帳號 密碼 
    /// </summary>
    public class UpdatePasswordDto
    {
        /// <summary>
        /// 權限群組 Id
        /// </summary>
        public string GroupId { get; set; }
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
        [PasswordVerify]
        public string OldSw { get; set; }
        /// <summary>
        /// 帳號狀態
        /// </summary>
        [RequiredCheck]
        [PasswordVerify]
        public string NewSw { get; set; }
    }
    public class HolidayRequestDto
    {
        /// <summary>
        /// 查詢日期
        /// </summary>
        public string SearchDate { get; set; }
    }
    public class ApiResponseDto
    {
        public string date { get; set; }
        public string year { get; set; }
        public string name { get; set; }
        public string isholiday { get; set; }
        public string holidaycategory { get; set; }
        public string description { get; set; }
    }
    public class HolidayResponseDto
    {
        /// <summary>
        /// 是否為假日
        /// </summary>
        public bool isHoliday { get; set; }
        /// <summary>
        /// 假日種類
        /// </summary>
        public string holidayCategory { get; set; }
    }
}
