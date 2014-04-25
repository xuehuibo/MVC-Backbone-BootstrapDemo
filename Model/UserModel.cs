using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class UserModel
    {
        public string OrgCode
        {
            get;
            set;
        }

        public string UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode
        {
            get;
            set;
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        public string ShopID
        {
            get;
            set;
        }
        /// <summary>
        /// 专柜编码
        /// </summary>
        public string ShopCode
        {
            get;
            set;
        }

        /// <summary>
        /// 专柜名称
        /// </summary>
        public string ShopName
        {
            get;
            set;
        }
    }
}