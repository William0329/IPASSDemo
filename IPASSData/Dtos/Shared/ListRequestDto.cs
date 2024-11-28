using System.ComponentModel;

namespace IPASSData.Dtos.Shared
{
    public class ListRequestDto
    {
        public ListRequestDto()
        {
            Page = 1;
            PageSize = 10;
        }
        /// <summary>
        /// 關鍵字
        /// </summary>
        public string? KeyWord { get; set; }
        /// <summary>
        /// 頁數
        /// </summary>
        [DefaultValue(1)]
        public int Page { get; set; }

        /// <summary>
        /// 筆數
        /// </summary>
        [DefaultValue(10)]
        public int PageSize { get; set; }
    }
}
