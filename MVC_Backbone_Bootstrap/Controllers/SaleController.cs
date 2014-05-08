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
    [AuthorizationAttribute]
    public class SaleController : Controller
    {
        //
        // GET: /Sale/
        public ViewResult SaleForm()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["Title"];
            return View();
        }

        public PartialViewResult PluList()
        {
            return PartialView();
        }

        public PartialViewResult ShowVip()
        {
            return PartialView();
        }

        public PartialViewResult ShowDetail()
        {
            return PartialView();
        }

        public PartialViewResult Invoice()
        {
            UserModel user = Session["LoginedUser"] as UserModel;
            ViewBag.Invoice_Title = ConfigurationManager.AppSettings["Invoice_Title"];
            ViewBag.Invoice_Remark = ConfigurationManager.AppSettings["Invoice_Remark"];
            ViewBag.ShopCode = user.ShopCode;
            ViewBag.ShopName = user.ShopName;
            ViewBag.UserCode = user.UserCode;
            ViewBag.UserName = user.UserName;
            return PartialView();
        }
    }
}
