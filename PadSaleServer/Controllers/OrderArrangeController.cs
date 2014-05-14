using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopSaleForPad.Attribute;

namespace ShopSaleForPad.Controllers
{
    [AuthorizationAttribute]
    public class OrderArrangeController : Controller
    {
        //
        // GET: /OrderArrange/

        public ActionResult OrderShow()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["Title"];
            return View();
        }

    }
}
