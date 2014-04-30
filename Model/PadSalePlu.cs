using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PadSalePlu
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
        /// 商品ID
        /// </summary>
        public string PluId
        {
            get;
            set;
        }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string PluCode
        {
            get;
            set;
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string PluName
        {
            get;
            set;
        }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal Price
        {
            get;
            set;
        }

        /// <summary>
        /// 会员价
        /// </summary>
        public decimal HyPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 现价
        /// </summary>
        public decimal FsPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 销售数量
        /// </summary>
        public decimal XsCount
        {
            get;
            set;
        }

        /// <summary>
        /// 销售金额
        /// </summary>
        public decimal XsTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣
        /// </summary>
        public decimal DisCountOff
        {
            get;
            set;
        }

        /// <summary>
        /// 备注，存储大码商品品名
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
    }
}
