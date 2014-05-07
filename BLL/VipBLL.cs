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
    public class VipBLL
    {
        /// <summary>
        /// 登录会员
        /// </summary>
        /// <param name="no">会员卡号或手机号</param>
        /// <returns></returns>
        public static ICollection<VipModel> LogonVip(string no,string orgCode,string connectionString)
        {
            try
            {
                using (OracleDAL dal = new OracleDAL(connectionString))
                {
                    OracleParameter orgCodeParam = new OracleParameter(":OrgCode", orgCode);
                    OracleParameter noParam = new OracleParameter(":No", no);
                    StringBuilder sql = new StringBuilder(256);
                    sql.Append(" Select A.VipCardNO,B.VipName,B.Gender,A.CardTypeCode,C.CardTypeName,B.Birthday,A.Regdate,A.Enddate,B.Mobile,B.Orgcode ");
                    sql.Append("  From tIsuCard A,tIsuVipInfo B ,tbascardtype C");
                    sql.Append(" Where A.Vipinfono=B.Vipinfono And A.CardTypeCode=C.CardTypeCode");
                    //sql.Append(" And B.Orgcode=:OrgCode ");
                    sql.Append(" And ( A.VipCardNO=:No Or B.Mobile=:No ) ");
                    int i;
                    //DataTable dt = dal.Select(sql.ToString(), out i, orgCodeParam, noParam);
                    DataTable dt = dal.Select(sql.ToString(), out i, noParam);
                    if (i > 0)
                    {
                        System.Collections.Generic.List<VipModel> vips = new System.Collections.Generic.List<VipModel>();
                        foreach (DataRow row in dt.Rows)
                        {
                            vips.Add(new VipModel()
                            {
                                VipCardNO = Convert.ToString(row["VipCardNO"]),
                                VipName = Convert.ToString(row["VipName"]),
                                Gender=Convert.ToString(row["Gender"]),
                                Regdate=Convert.ToDateTime(row["Regdate"]).ToString("yyyy-MM-dd"),
                                Enddate=Convert.ToDateTime(row["Enddate"]).ToString("yyyy-MM-dd"),
                                CardTypeCode = Convert.ToString(row["CardTypeCode"]),
                                CardTypeName=Convert.ToString(row["CardTypeName"]),
                                Birthday = Convert.ToDateTime(row["Birthday"]).ToString("yyyy-MM-dd"),
                                Mobile = Convert.ToString(row["Mobile"])
                            });
                        }
                        return vips;
                    }
                    else
                    {
                        throw new Exception("读取会员失败！");
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
