using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace IPASSDemo.Helpers
{
    /// <summary>
    /// 加密演算法
    /// </summary>
    public class EncryptHelper
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EncryptHelper> _logger;
        public EncryptHelper(IConfiguration configuration, ILogger<EncryptHelper> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        /// <summary>
        /// 金鑰式雜湊演算法256加密
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string HMACSHA256(string message, string key)
        {
            var encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacSHA256 = new HMACSHA256(keyByte))
            {
                byte[] hashMessage = hmacSHA256.ComputeHash(messageBytes);
                return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
            }
        }


        /// <summary>
        /// AES256加密
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <param name="mode"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public string A256Encrypt(string message, string key, CipherMode mode, byte[] iv = null)
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = iv ?? new byte[16];
                aesAlg.Mode = mode;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(message);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        /// <summary>
        /// AES256解密
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <param name="mode"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public string A256Decrypt(string message, string key, CipherMode mode, byte[] iv = null)
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = iv ?? new byte[16];
                aesAlg.Mode = mode;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(message)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// MD5 32大寫加密 UTF-8編碼
        /// </summary>
        /// <param name="value">要加密的值</param>   
        /// <returns></returns>
        public string UserMd5(string value)
        {
            string referenceId = string.Empty;

            // 實作md5
            MD5 md5 = MD5.Create();

            // 加密後是一個字元類型的陣列, 要注意編碼UTF8/Unicode
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

            // 通過迴圈, 將字元陣列轉字串
            for (int i = 1; i < s.Length; i++)
            {
                // 將得到32大寫位元加密。格式後的字串是小寫字母, 如果要大寫字母則使用大寫(X)
                referenceId += s[i].ToString("X2");
            }

            return referenceId;
        }
        /// <summary>
        /// SHA256 加密密碼
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HMACSHA256Password(string password)
        {
            //加密時加入的Salt
            string _authenticationSalt = _configuration.GetValue<string>("Authentication:Salt")!;

            //使用者加密後的密碼
            string _userPassword = HMACSHA256(password, _authenticationSalt);

            return _userPassword;
        }

        /// <summary>
        /// AES256解密後,反序列化(Deserialize)
        /// </summary>
        /// <typeparam name="T"> Object類別名 </typeparam>
        /// <param name="encryptedData"> 加密字串 </param>
        /// <param name="salt"> 加密金鑰 </param>
        /// <returns></returns>
        public async Task<T> DecryptAES256Deserialize<T>(string encryptedData, string salt = null)
        {
            //取得加密金鑰
            string _salt = _configuration.GetValue<string>(salt);

            string _decrypteContent = string.Empty; ;

            try
            {
                _logger.LogInformation($"AES256未解密 : {encryptedData}");
                //內容解密
                _decrypteContent = A256Decrypt(encryptedData, _salt, CipherMode.CBC, null);

                _logger.LogInformation($"AES256解密 = {_decrypteContent}");
            }
            catch
            {
                _logger.LogInformation($"解密AES256發生錯誤 = {_decrypteContent}");
                throw new Exception("解密發生錯誤");
            }

            //反序列化
            return JsonConvert.DeserializeObject<T>(_decrypteContent);
        }
    }
}
