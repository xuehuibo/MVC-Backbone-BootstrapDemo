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
    public class SaleApiController : ApiController
    {
        // GET api/saleapi
        public IEnumerable<PadSale> Get(string code, string PayStatus, string SaleSource,string isTook, string orderBy,int pageNo,int pageSize)
        {
            UserModel user = HttpContext.Current.Session["LoginedUser"] as UserModel;
            try
            {
                return SaleBLL.GetSaleData(code, PayStatus, SaleSource,isTook,orderBy,pageNo,pageSize, user, 
                    ConfigurationManager.ConnectionStrings["CMP_DBConnection"].ConnectionString);
            }
            catch
            {
                return new PadSale[]{};
            }
        }
    }
}
