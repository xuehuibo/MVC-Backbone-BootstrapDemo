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
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ViewResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public PartialViewResult Logon()
        {
            return new PartialViewResult();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="signinModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public JsonResult Signin(SigninModel signinModel)
        {
            ResultModel rst = new ResultModel();
            try
            {
                UserModel user = SigninBLL.Signin(signinModel,ConfigurationManager.AppSettings["OrgCode"], ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
                //Session.Add("User", user);
                HttpContext.Session.Add("LoginedUser", user);
                rst.Rst = 1;
                rst.Msg = "登陆成功!";
            }
            catch (Exception ex)
            {
                Session.Remove("User");
                if (ex.GetType() == typeof(Exception))
                {
                    rst.Rst = 0;
                }
                else
                {
                    rst.Rst = -1;
                }
                rst.Msg = ex.Message;
            }
            return Json(rst);
        }
    }
}
