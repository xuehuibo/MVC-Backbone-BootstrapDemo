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
using Newtonsoft.Json;

namespace ShopSaleForPad.Controllers
{
    public class PluApiController : ApiController
    {
        // GET api/pluapi

        public IEnumerable<PluModel> Get(int filterType,string filterValue)
        {
            UserModel user = HttpContext.Current.Session["LoginedUser"] as UserModel;
            //UserModel user = JsonConvert.DeserializeObject<UserModel>(userJson);

            if (user == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return PluBLL.GetPluList(user.OrgCode, user.ShopID, filterType, filterValue, ConfigurationManager.ConnectionStrings["CMP_DBConnection"].ConnectionString);
                }
                catch 
                {
                    return null;
                }
            }
        }
    }
}
