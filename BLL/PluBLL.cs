using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data;
using System.Data.OracleClient;

namespace BLL
{
    public class PluBLL
    {
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="orgCode">组织代码</param>
        /// <param name="shpId">专柜ID</param>
        /// <returns></returns>
        public static ICollection<PluModel> GetPluList(string orgCode, int page,int pageSize,string shpId,string ConnecionString)
        {
            try
            {
                DataTable dt;
                using (OracleDAL dal = new OracleDAL(ConnecionString))
                {
                    StringBuilder sql = new StringBuilder(256);
                    sql.Append("Select * from (Select Rownum as SerialNo,A.* From  RvSkuForPad A ");
                    sql.Append(" Where OrgCode=:OrgCode And ShpId=:ShpId ) ");
                    sql.AppendFormat(" Where SerialNo Between {0} and {1} ", 
                        ((page - 1) * pageSize+1).ToString(), (page * pageSize).ToString());
                    dt = dal.Select(sql.ToString(), new OracleParameter(":OrgCode", orgCode),new OracleParameter(":ShpId",shpId));
                }
                ICollection<PluModel> pluList = new List<PluModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    pluList.Add(new PluModel()
                    {
                        OrgCode = Convert.ToString(dr["OrgCode"]),
                        DepId = Convert.ToString(dr["DepId"]),
                        DepCode = Convert.ToString(dr["DepCode"]),
                        ShpId = Convert.ToString(dr["ShpId"]),
                        ShpCode = Convert.ToString(dr["ShpCode"]),
                        PluId = Convert.ToString(dr["PluId"]),
                        PluCode = Convert.ToString(dr["PluCode"]),
                        PluName = Convert.ToString(dr["PluName"]),
                        BarCode = Convert.ToString(dr["BarCode"]),
                        Unit = Convert.ToString(dr["Unit"]),
                        Spec = Convert.ToString(dr["Spec"]),
                        Price = Convert.ToDecimal(dr["Price"]),
                        HyPrice = Convert.ToDecimal(dr["HyPrice"])
                    });
                }
                return pluList;
            }
            catch
            {
                throw;
            }
        }
    }
}
