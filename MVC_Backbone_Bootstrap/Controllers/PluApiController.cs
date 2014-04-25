using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.SessionState;
using ShopSaleForPad.Attribute;
using BLL;
using System.Configuration;

namespace ShopSaleForPad.Controllers
{
    [AuthorizationAttribute]
    public class PluApiController : ApiController
    {
        // GET api/pluapi

        public IEnumerable<PluModel> Get(int filterType,string filterValue)
        {
            UserModel user = HttpContext.Current.Session["LoginedUser"] as UserModel;

            if (user == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return PluBLL.GetPluList(user.OrgCode, user.ShopID, filterType, filterValue, ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
                }
                catch 
                {
                    return null;
                }
            }
        }
    }
}
