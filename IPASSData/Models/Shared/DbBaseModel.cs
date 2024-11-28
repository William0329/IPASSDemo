using System.ComponentModel.DataAnnotations;

namespace IPASSData.Models.Shared
{
    /// <summary>
    /// Db 基礎 共用項目
    /// </summary>
    public class DbBaseModel
    {
        /// <summary>
        /// 使用者 Id
        /// </summary>
        [Key]
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// 建立者 Id
        /// </summary>
        public Guid CreateBy { get; set; }

        /// <summary>
        /// 刪除者 Id
        /// </summary>
        public Guid? DeleteBy { get; set; }

        /// <summary>
        /// 更新者 Id
        /// </summary>
        public Guid? UpdateBy { get; set; }

        /// <summary>
        /// 是否刪除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 創建日期
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdateAt { get; set; }
    }
}
