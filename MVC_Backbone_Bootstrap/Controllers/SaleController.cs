using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using System.Configuration;
using Newtonsoft.Json;
using ShopSaleForPad.Attribute;

namespace ShopSaleForPad.Controllers
{
    [Authorization]
    public class SaleController : Controller
    {
        //
        // GET: /Sale/
        public ViewResult SaleForm()
        {
            return View();
        }

        public PartialViewResult PluView()
        {
            return new PartialViewResult();
        }
    }
}
