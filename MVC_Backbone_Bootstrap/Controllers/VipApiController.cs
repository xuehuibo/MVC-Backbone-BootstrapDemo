using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Model;
using BLL;
using System.Configuration;
using Newtonsoft.Json;

namespace ShopSaleForPad.Controllers
{
    public class VipApiController : ApiController
    {
        // GET api/vipapi
        public IEnumerable<VipModel> Get(string no)
        {
            UserModel user = HttpContext.Current.Session["LoginedUser"] as UserModel;
            //UserModel user = JsonConvert.DeserializeObject<UserModel>(userJson);
            if (user == null)
            {
                return null;
            }

            try
            {
                return VipBLL.LogonVip(no, user.OrgCode, ConfigurationManager.ConnectionStrings["CRM_DBConnection"].ConnectionString);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET api/vipapi/5
        public VipModel Get(int id)
        {
            VipModel vip=new VipModel();
            return vip;
        }
    }
}
