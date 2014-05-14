using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PadSale
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
        /// 专柜ID
        /// </summary>
        public decimal ShpId
        {
            get;
            set;
        }

        /// <summary>
        /// 专柜CODE
        /// </summary>
        public string ShpCode
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ShpName
        {
            get;
            set;
        }

        /// <summary>
        /// 营业员ID
        /// </summary>
        public decimal ClerkId
        {
            get;
            set;
        }

        /// <summary>
        /// 营业员编码
        /// </summary>
        public string ClerkCode
        {
            get;
            set;
        }

        public string ClerkName
        {
            get;
            set;
        }

        /// <summary>
        /// 开票生成时间
        /// </summary>
        public DateTime BuildDate
        {
            get;
            set;
        }

        /// <summary>
        /// 会员卡号
        /// </summary>
        public string VipCardNo
        {
            get;
            set;
        }

        /// <summary>
        /// 总交易金额
        /// </summary>
        public decimal XsTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 待支付金额
        /// </summary>
        public decimal NeedPayTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 0-未支付   1-部分支付  2-已支付
        /// </summary>
        public string PayStatus
        {
            get;
            set;
        }

        public string PayStatusName
        {
            get;
            set;
        }

        /// <summary>
        /// POS前台支付金额
        /// </summary>
        public decimal PosPayTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 前台支付时间
        /// </summary>
        public DateTime PosPayTime
        {
            get;
            set;
        }

        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 是否有效，为0的表示已经删除
        /// </summary>
        public string IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// 是否已提货，0-未提，1-已提
        /// </summary>
        public string IsTook
        {
            get;
            set;
        }

        /// <summary>
        /// 0-Pad开票 其他预留
        /// </summary>
        public string SaleSource
        {
            get;
            set;
        }

        public string SaleSourceName
        {
            get;
            set;
        }

        /// <summary>
        /// 商品明细列表
        /// </summary>
        public ICollection<PadSalePlu> SalePlu
        {
            get;
            set;
        }
    }
}
