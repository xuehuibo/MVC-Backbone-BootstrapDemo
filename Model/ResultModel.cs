using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ResultModel
    {
        /// <summary>
        /// 处理结果
        /// 成功-1  失败-0
        /// </summary>
        public int Rst;

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string Msg;

        /// <summary>
        /// 结果Json
        /// </summary>
        public object Obj;
    }
}
