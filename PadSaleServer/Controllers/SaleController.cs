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
using System.Drawing;
using Tools;
using System.IO;
using System.Drawing.Imaging;
using ShopSaleForPad.Tools;
namespace ShopSaleForPad.Controllers
{
    [AuthorizationAttribute]
    public class SaleController : Controller
    {
        //
        // GET: /Sale/
        public ViewResult SaleForm()
        {
            UserModel user = Session["LoginedUser"] as UserModel;
            ViewBag.UserCode = user.UserCode;
            ViewBag.UserName = user.UserName;
            ViewBag.ShopName = user.ShopName;

            ViewBag.Title = ConfigurationManager.AppSettings["Title"];
            try
            {
                string connectionString=ConfigurationManager.ConnectionStrings["CMP_DBConnection"].ConnectionString;
                IEnumerable<EnumModel> payStatus = SaleBLL.GetPayStatus(connectionString);
                IEnumerable<EnumModel> saleSource = SaleBLL.GetSaleSource(connectionString);
                ViewBag.payStatus = payStatus;
                ViewBag.saleSource = saleSource;
                return View();
            }
            catch 
            { 
                return View(); 
            }
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

        /// <summary>
        /// 获取一维码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public FileResult Barcode1v(string code)
        {
            Bitmap bitMap = Code128.GetCodeImage(code, Code128.Encode.Code128B);
            MemoryStream ms = new MemoryStream();
            bitMap.Save(ms, ImageFormat.Jpeg);
            return new FileContentResult(ms.ToArray(),"image/jpeg");
        }

        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public FileResult Barcode2v(string code)
        {
            Bitmap bitMap = QrCode.CreateCode(code, ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE,
                ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.M, 5, 5);
            MemoryStream ms = new MemoryStream();
            bitMap.Save(ms, ImageFormat.Jpeg);
            return new FileContentResult(ms.ToArray(), "image/jpeg");
        }

        /// <summary>
        /// 开票
        /// </summary>
        /// <param name="sale"></param>
        /// <param name="salePlu"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DoInvoice(string sale)
        {
            UserModel user = Session["LoginedUser"] as UserModel;
            ResultModel rst = new ResultModel();
            if (user == null)
            {
                rst.Rst = 0;
                rst.Msg = "用户登录过期";
                return Json(rst);
            }

            PadSale padSale = JsonConvert.DeserializeObject<PadSale>(sale);

            try
            {
                rst.Obj=SaleBLL.DoInvoice(padSale, user, ConfigurationManager.ConnectionStrings["CMP_DBConnection"].ConnectionString);
                rst.Rst = 1;
                rst.Msg = "ok";
                return Json(rst);
            }
            catch(Exception ex)
            {
                rst.Rst = 0;
                rst.Msg = ex.Message;
                return Json(rst);
            }
        }

        /// <summary>
        /// 提货
        /// </summary>
        /// <param name="padSaleNo"></param>
        /// <returns></returns>
        public JsonResult DoTakeGoods(string padSaleNo)
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
                rst.Rst = SaleBLL.TakeGoods(padSaleNo, user,ConfigurationManager.ConnectionStrings["CMP_DBConnection"].ConnectionString, out rst.Msg) ? 1 : 0;
                return Json(rst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                rst.Rst = -1;
                rst.Msg = ex.Message;
                return Json(rst, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 流水绑定手牌号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BindHandCard(string saleNo,string handCard)
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
                rst.Rst = SaleBLL.BindHandCard(user.OrgCode, user.ShopID, handCard, saleNo,
                    ConfigurationManager.ConnectionStrings["CMP_DBConnection"].ConnectionString, out rst.Msg) ? 1 : 0;
                return Json(rst);
            }
            catch (Exception ex)
            {
                rst.Rst = -1;
                rst.Msg = ex.Message;
                return Json(rst);
            }
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="saleNo"></param>
        /// <param name="mobileNum"></param>
        /// <returns></returns>
        public JsonResult SendSMS(string saleNo,string mobileNum)
        {
            UserModel user = Session["LoginedUser"] as UserModel;
            ResultModel rst = new ResultModel();
            if (user == null)
            {
                rst.Rst = 0;
                rst.Msg = "用户登录失效";
                return Json(rst);
            }
            rst = new ResultModel();
            rst.Rst = SaleBLL.SendSMS(saleNo, mobileNum, out rst.Msg);
            return Json(rst);
        }

        /// <summary>
        /// 发送微信
        /// </summary>
        /// <param name="saleNo"></param>
        /// <returns></returns>
        public JsonResult SendMicroMessage(string saleNo)
        {
            return null;
        }


    }
}
