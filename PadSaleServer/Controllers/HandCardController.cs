using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using ShopSaleForPad.Attribute;

namespace ShopSaleForPad.Controllers
{
    [AuthorizationAttribute]
    public class HandCardController : Controller
    {
        //
        // GET: /HandCard/

        public ActionResult HandCardShow()
        {
            UserModel user = Session["LoginedUser"] as UserModel;
            ViewBag.UserCode = user.UserCode;
            ViewBag.UserName = user.UserName;
            ViewBag.ShopName = user.ShopName;
            ViewBag.Title = ConfigurationManager.AppSettings["Title"];
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handcard"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddNew(string handCard, string barCode, string remark)
        {
            UserModel user = Session["LoginedUser"] as UserModel;
            ResultModel rst = new ResultModel();
            if (user == null)
            {
                rst.Rst = 0;
                rst.Msg = "用户登录失效";
                return Json(rst);
            }
            try
            {
                rst.Rst = HandCardBLL.AddHandCard(
                    new PadHandCard()
                    {
                        ShpId=user.ShopID,
                        HandCard=handCard,
                        BarCode=barCode,
                        Remark=remark,
                        CardStatus="0"

                    }, ConfigurationManager.ConnectionStrings["CMP_DBConnection"].ConnectionString) ? 1 : 0;
                return Json(rst);
            }
            catch(Exception ex)
            {
                rst.Rst = -1;
                rst.Msg = ex.Message;
                return Json(rst);
            }
        }

        [HttpPost]
        public JsonResult Del(string handCard)
        {
            UserModel user = Session["LoginedUser"] as UserModel;
            ResultModel rst = new ResultModel();
            if (user == null)
            {
                rst.Rst = 0;
                rst.Msg = "用户登录失效";
                return Json(rst);
            }
            try
            {
                rst.Rst=HandCardBLL.DelHandCard(handCard, user.ShopID, ConfigurationManager.ConnectionStrings["CMP_DBConnection"].ConnectionString,out rst.Msg)?1:0;
                return Json(rst);
            }
            catch(Exception ex)
            {
                rst.Rst = -1;
                rst.Msg = ex.Message;
                return Json(rst);
            }
        }
    }
}
