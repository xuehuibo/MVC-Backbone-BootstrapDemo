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
        public string DepId
        {
            get;
            set;
        }

        public string DepCode
        {
            get;
            set;
        }

        public string ShpId
        {
            get;
            set;
        }

        public string ShpCode
        {
            get;
            set;
        }

        public string PluId
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

        private string pluImage;
        public string PluImage
        {
            set
            {
                pluImage = value;
            }
            get
            {
                if (string.IsNullOrEmpty(pluImage))
                {
                    return "default";
                }
                else
                {
                    return pluImage;
                }
            }
        }
    }
}
