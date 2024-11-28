namespace IPASSData.Dtos.Authentication
{
    /// <summary>
    /// 登出 請求
    /// </summary>
    public class LogoutRequestDto : JWTModel
    {

    }
    /// <summary>
    /// 登出 回應
    /// </summary>
    public class LogoutResponseDto
    {
        /// <summary>
        /// 回應訊息
        /// </summary>
        public string Message { get; set; }
    }
}
