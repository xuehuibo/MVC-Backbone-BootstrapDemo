using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PadHandCard
    {
        /// <summary>
        /// 手牌号
        /// </summary>
        public string HandCard
        {
            get;
            set;
        }

        /// <summary>
        /// 手牌条码
        /// </summary>
        public string BarCode
        {
            get;
            set;
        }

        /// <summary>
        /// 手牌状态  
        /// 0-未占用，1-已占用
        /// </summary>
        public string CardStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 当前使用的专柜
        /// </summary>
        public string ShpId
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
    }
}
