using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PadSalePay
    {
        /// <summary>
        /// 开票唯一标识
        /// </summary>
        public decimal PadSaleNo
        {
            get;
            set;
        }

        /// <summary>
        /// 组织号
        /// </summary>
        public string OrgCode
        {
            get;
            set;
        }

        /// <summary>
        /// 行号，唯一标识
        /// </summary>
        public int LnNo
        {
            get;
            set;
        }

        /// <summary>
        /// 支付方式编码
        /// </summary>
        public string PayCode
        {
            get;
            set;
        }

        /// <summary>
        /// 支付方式名称
        /// </summary>
        public string PayName
        {
            get;
            set;
        }

        /// <summary>
        /// 支付号
        /// </summary>
        public string PayNo
        {
            get;
            set;
        }

        /// <summary>
        /// 支付金额
        /// </summary>
        public string PayTotal
        {
            get;
            set;
        }

        public string Remark
        {
            get;
            set;
        }
    }
}
