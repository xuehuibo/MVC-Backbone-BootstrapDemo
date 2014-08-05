using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 商品模板 
    /// </summary>
    public class PluModel
    {
        public string OrgCode
        {
            get;
            set;
        }
        public decimal DepId
        {
            get;
            set;
        }

        public string DepCode
        {
            get;
            set;
        }

        public decimal ShpId
        {
            get;
            set;
        }

        public string ShpCode
        {
            get;
            set;
        }

        public decimal PluId
        {
            get;
            set;
        }

        public string PluCode
        {
            get;
            set;
        }

        public string PluName
        {
            get;
            set;
        }

        public string BarCode
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            set;
        }

        public string Spec
        {
            get;
            set;
        }

        public decimal? Price
        {
            get;
            set;
        }

        public decimal? HyPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 商品说明
        /// </summary>
        public string Brief
        {
            get;
            set;
        }
    }
}
