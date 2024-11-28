using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IPASSData.Dtos.Shared
{
    /// <summary>
    /// 分頁 模型 
    /// </summary>
    public class PageModel
    {
        public PageModel()
        {
            page = 1;
            pageSize = 10;
        }
        /// <summary>
        /// 頁碼
        /// </summary>
        [Required, DefaultValue(1), Display(Name = "page")]
        public int page { get; set; }
        /// <summary>
        /// 顯示筆數
        /// </summary>
        [Required, DefaultValue(10), Display(Name = "pageSize")]
        public int pageSize { get; set; }
        /// <summary>
        /// 查詢字串
        /// </summary>
        [Display(Name = "keyWord")]
        public string? keyWord { get; set; }
    }
}
