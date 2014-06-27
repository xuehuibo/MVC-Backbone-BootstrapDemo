using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YeionSMS
{
    public class YeionSMSClass
    {
        const string SMS_URL = "http://mt.549k.com/send.do?Account=@Account&Password=@Password&Mobile=@Mobile&Content=@Content&Exno=0";
        /// <summary>
        /// 生成短信请求
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string BuilderUrl(string mobile,string content)
        {

            StringBuilder message = new StringBuilder(512);
            message.Append("短信模板");//在此生成短信
            StringBuilder sms = new StringBuilder(SMS_URL, 350);
            sms.Replace("@Mobile", mobile);
            sms.Replace("@Content", content);
            sms.Replace("@Account", "hxgc");
            sms.Replace("@Password", "123456");
            return sms.ToString();
        }
    }
}
