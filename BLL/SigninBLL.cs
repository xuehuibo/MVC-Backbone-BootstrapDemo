using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data.OracleClient;
using System.Data;

namespace BLL
{
    public class SigninBLL
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="signModel"></param>
        /// <param name="connectionStr"></param>
        /// <returns></returns>
        public static UserModel Signin(SigninModel signModel,string orgCode, string connectionStr)
        {
            try{
                using (OracleDAL dal = new OracleDAL(connectionStr))
                {
                    DataTable dt =
                        dal.Select(" Select  UserId,UserCode,UserName,ShpId,ShpCode,ShpName "
                                + " From tUsrUser A "
                                + " Where Exists(Select 1 From tUsrEmployee Where EmpId=A.EmpId And RoleCode='08' And XzOrgCode=:OrgCode) " //仅营业员角色可使用
                                + " And ShpCode=:ShopCode "
                                + " And Exists(Select 1 From tShpShoppe Where ShpId=A.ShpId And IsActive='1'And OrgCode=:OrgCode) "
                                + " And IsActive='1' "
                                + " And UserCode=:UserCode ",
                            new OracleParameter(":UserCode", signModel.UserCode),
                            new OracleParameter(":ShopCode", signModel.ShopCode),
                            new OracleParameter(":OrgCode", orgCode));
                    if (dt.Rows.Count > 0)
                    {
                        return new UserModel()
                        {
                            UserID = Convert.ToString(dt.Rows[0]["UserId"]),
                            UserCode = Convert.ToString(dt.Rows[0]["UserCode"]),
                            UserName = Convert.ToString(dt.Rows[0]["UserName"]),
                            ShopID = Convert.ToString(dt.Rows[0]["ShpId"]),
                            ShopCode = Convert.ToString(dt.Rows[0]["ShpCode"]),
                            ShopName = Convert.ToString(dt.Rows[0]["ShpName"])
                        };
                    }
                    else
                    {
                        throw new Exception("用户名或密码错误！",null);
                    }
                }
            }
            catch{
                throw;
            }
        } 
    }
}
