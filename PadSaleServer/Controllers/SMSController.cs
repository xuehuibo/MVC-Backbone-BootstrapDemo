using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using System.Configuration;
using YeionSMS;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace ShopSaleForPad.Controllers
{
    public class SMSController : Controller
    {
        //
        // Get: /SMS/
        [HttpPost]
        public JsonResult SendSMS(string saleNo,string mobile)
        {
            UserModel user = Session["LoginedUser"] as UserModel;
            ResultModel rst = new ResultModel();

            try
            {
                PadSale padSale = SaleBLL.GetSaleData(saleNo, user,ConfigurationManager.ConnectionStrings["CMP_DBConnection"].ConnectionString);//交易信息
                
                StringBuilder sms = new StringBuilder(350);
                sms.AppendFormat("尊敬的顾客，您于{0}选购了{1}件商品，流水号{2},计{3}元。请携短信至银台交款/青岛海信广场",
                    padSale.BuildDate.ToString("yyyy年mm日HH时mm分"),
                    padSale.SalePlu.Count.ToString(),
                     padSale.PadSaleNo,
                    padSale.XsTotal.ToString("F2"));
                string url="";
                switch (ConfigurationManager.AppSettings["SMS"])
                {
                    case "Yeion":
                        url = YeionSMSClass.BuilderUrl(mobile, sms.ToString());
                        break;

                }
                WebRequest wr = WebRequest.CreateDefault(new Uri(url));
                HttpWebResponse hwr= wr.GetResponse() as HttpWebResponse;
                string content = new StreamReader(hwr.GetResponseStream()).ReadToEnd();
                Result result = JsonConvert.DeserializeObject<Result>(content);
                if (result.code == "9001")
                {
                    rst.Rst = 1;
                    rst.Msg = "发送成功";
                }
                else
                {
                    rst.Rst = 0;
                    rst.Msg = result.message;
                }
                
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
