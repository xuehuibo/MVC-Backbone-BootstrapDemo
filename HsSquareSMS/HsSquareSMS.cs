using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UInterface;

namespace HsSquareSMS
{
    /// <summary>
    /// 短信平台类
    /// </summary>
    public class HsSquareSMS : ISendSMS
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="saleNo"></param>
        /// <param name="mobileNum"></param>
        /// <returns></returns>
        public int Send(string saleNo, string mobileNum,params object[] values )
        {
            return null;
        }
    }
    
}
