using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace UInterface
{
    /// <summary>
    /// 短信平台接口
    /// </summary>
    public interface ISendSMS
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="saleNo"></param>
        /// <param name="mobileNum"></param>
        /// <returns></returns>
        [Description("发送短信")]
        int Send(string saleNo, string mobileNum ,,params object[] values );

    }
}
