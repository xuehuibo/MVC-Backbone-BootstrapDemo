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

namespace ShopSaleForPad.Controllers
{
    public class HandCardApiController : ApiController
    {
        // GET api/handcardapi
        public IEnumerable<PadHandCard> Get()
        {
            UserModel user = HttpContext.Current.Session["LoginedUser"] as UserModel;
            if (user == null)
            {
                return null;
            }
            try
            {
                return HandCardBLL.ListHandCard(user.ShopID, ConfigurationManager.ConnectionStrings["CMP_DBConnection"].ConnectionString);
            }
            catch
            {
                return null;
            }
        }
    }
}
