using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Newtonsoft.Json;
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
            UserModel user = Session["LoginedUser"] as UserModel;
            ViewBag.UserCode = user.UserCode;
            ViewBag.UserName = user.UserName;
            ViewBag.ShopName = user.ShopName;
            ViewBag.Title = ConfigurationManager.AppSettings["Title"];
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CMP_DBConnection"].ConnectionString;
                IEnumerable<EnumModel> payStatus = SaleBLL.GetPayStatus(connectionString);
                IEnumerable<EnumModel> saleSource = SaleBLL.GetSaleSource(connectionString);
                ViewBag.payStatus = payStatus;
                ViewBag.saleSource = saleSource;
            }
            catch
            {
            }
            return View();
        }

        /// <summary>
        /// 获取汇总
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetSaleTotal()
        {
            ResultModel rst = new ResultModel();
            UserModel user = Session["LoginedUser"] as UserModel;
            try
            {
                rst.Obj = SaleBLL.GetPadSaleTotal(user, ConfigurationManager.ConnectionStrings["CMP_DBConnection"].ConnectionString);
                rst.Rst = 1;
                return Json(rst,JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                rst.Rst = 0;
                rst.Msg = ex.Message;
                return Json(rst, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CancelOrder(string padSaleNo)
        {
            UserModel user= Session["LoginedUser"] as UserModel;
            ResultModel rst = new ResultModel();
            try
            {
                rst.Rst = SaleBLL.CancelOrder(user, padSaleNo, ConfigurationManager.ConnectionStrings["CMP_DBConnection"].ConnectionString)?1:0;
                return Json(rst);
            }
            catch (Exception ex)
            {
                rst.Rst = -1;
                rst.Msg = ex.Message;
                return Json(rst);
            }
        }
    }
}
