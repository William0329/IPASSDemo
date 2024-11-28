namespace IPASSData.Dtos.Shared
{
    /// <summary>
    /// 回應模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseModel<T>
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
        public T ResponseData { get; set; }
    }

    /// <summary>
    /// 回應模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseModelByLnData<T>
    {
        /// <summary>
        /// 回傳資料
        /// </summary>
        public T ResponseData { get; set; }
    }
}
