using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopSaleForPad.Controllers
{
    public class SaleController : Controller
    {
        //
        // GET: /Sale/

        public ActionResult SaleForm()
        {
            return View();
        }

        public PartialViewResult PluView()
        {
            return new PartialViewResult();
        }
    }
}
