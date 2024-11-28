namespace IPASSData.Extensions
{
    /// <summary>
    /// DateTime 擴充
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 日期時間數字字串
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToNumDateTimeString(this DateTime date)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        /// <summary>
        /// 時間數字字串
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToNumTimeString(this DateTime date)
        {
            return DateTime.Now.ToString("HHmmss");
        }
        /// <summary>
        /// 日期字串
        /// </summary>
        /// <param name="date"></param>
        /// <param name="spilt"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime date, string spilt = "-")
        {
            return date.ToString($"yyyy{spilt}MM{spilt}dd");
        }

        /// <summary>
        /// 日期時間字串
        /// </summary>
        /// <param name="date"></param>
        /// <param name="spilt"></param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTime date, string spilt = "-")
        {
            return date.ToString($"yyyy{spilt}MM{spilt}dd HH:mm:ss");
        }
    }
}
