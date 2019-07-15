using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MshErp.Infrastructure
{
    public class ConfigHelper
    {
        /// <summary>
        /// Secrect
        /// </summary>
        public static string SystemSecrect
        {
            get
            {
                var value = ConfigurationManager.AppSettings[$"{nameof(SystemSecrect)}"];
                if (string.IsNullOrEmpty(value))
                    throw new Exception($"请在配置文件中配置{nameof(SystemSecrect)}");
                return value;
            }
        }

        /// <summary>
        /// Token过期时间
        /// </summary>
        public static string TokenExpireMinutes
        {
            get
            {
                var value = ConfigurationManager.AppSettings[$"{nameof(TokenExpireMinutes)}"];
                if (string.IsNullOrEmpty(value))
                    throw new Exception($"请在配置文件中配置{nameof(TokenExpireMinutes)}");
                return value;
            }
        }
        public static string WeiXinAppid
        {
            get
            {
                var value = ConfigurationManager.AppSettings[$"{nameof(WeiXinAppid)}"];
                if (string.IsNullOrEmpty(value))
                    throw new Exception($"请在配置文件中配置{nameof(WeiXinAppid)}");
                return value;
            }
        }
        public static string WeiXinSecret
        {
            get
            {
                var value = ConfigurationManager.AppSettings[$"{nameof(WeiXinSecret)}"];
                if (string.IsNullOrEmpty(value))
                    throw new Exception($"请在配置文件中配置{nameof(WeiXinSecret)}");
                return value;
            }
        }

        public static string BaiduAppID
        {
            get
            {
                var value = ConfigurationManager.AppSettings[$"{nameof(BaiduAppID)}"];
                if (string.IsNullOrEmpty(value))
                    throw new Exception($"请在配置文件中配置{nameof(BaiduAppID)}");
                return value;
            }
        }

        public static string BaiduApiKey
        {
            get
            {
                var value = ConfigurationManager.AppSettings[$"{nameof(BaiduApiKey)}"];
                if (string.IsNullOrEmpty(value))
                    throw new Exception($"请在配置文件中配置{nameof(BaiduApiKey)}");
                return value;
            }
        }

        public static string BaiduSecretKey
        {
            get
            {
                var value = ConfigurationManager.AppSettings[$"{nameof(BaiduSecretKey)}"];
                if (string.IsNullOrEmpty(value))
                    throw new Exception($"请在配置文件中配置{nameof(BaiduSecretKey)}");
                return value;
            }
        }
    }
}
