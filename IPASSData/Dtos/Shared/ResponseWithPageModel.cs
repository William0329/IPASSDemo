namespace IPASSData.Dtos.Shared
{
    public class ResponseWithPageModel<T>
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 回傳資料
        /// </summary>
        public ResponseWithPageDataModel<T> ResponseData { get; set; }
    }

    public class ResponseWithPageDataModel<T>
    {
        public int Total { get; set; }

        public T Rows { get; set; }
    }
}
