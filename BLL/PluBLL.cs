﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data;
using System.Data.OracleClient;
using System.IO;

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
        public static ICollection<PluModel> GetPluList(string orgCode,string shpId,int? filterType,string filterValue,string ConnecionString)
        {
            try
            {
                DataTable dt;
                using (OracleDAL dal = new OracleDAL(ConnecionString))
                {
                    dal.Open();
                    OracleParameter orgCodeParam= new OracleParameter(":OrgCode", orgCode);
                    OracleParameter shpIdParam=new OracleParameter(":ShpId",shpId);
                    StringBuilder sql = new StringBuilder(256);
                    sql.Append(" Select A.*,B.BRIEF From vSkuForPad A left join tskupluimage B on  A.PluId=b.pluid ");
                    sql.Append(" Where OrgCode=:OrgCode And ShpId=:ShpId ");
                    if (filterType == 1 && !string.IsNullOrEmpty(filterValue))
                    {
                        sql.AppendFormat(" And ( PluCode like '%{0}%' ", filterValue);
                        sql.AppendFormat(" Or BarCode like '%{0}%' ", filterValue);
                        sql.AppendFormat(" Or Upper(PluName) like '%{0}%' ) ", filterValue.ToUpper());
                    }
                    int i;
                    dt = dal.Select(sql.ToString(),out i, orgCodeParam,shpIdParam);
                }
                ICollection<PluModel> pluList = new List<PluModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    pluList.Add(new PluModel() {
                        OrgCode = Convert.ToString(dr["OrgCode"]),
                        DepId = Convert.ToDecimal(dr["DepId"]),
                        DepCode = Convert.ToString(dr["DepCode"]),
                        ShpId = Convert.ToDecimal(dr["ShpId"]),
                        ShpCode = Convert.ToString(dr["ShpCode"]),
                        PluId = Convert.ToDecimal(dr["PluId"]),
                        PluCode = Convert.ToString(dr["PluCode"]),
                        PluName = Convert.ToString(dr["PluName"]),
                        BarCode = Convert.ToString(dr["BarCode"]),
                        Unit = Convert.ToString(dr["Unit"]),
                        Spec = Convert.ToString(dr["Spec"]),
                        Price = Convert.ToDecimal(dr["Price"]),
                        HyPrice = Convert.ToDecimal(dr["HyPrice"]),
                        Brief=Convert.ToString(dr["BRIEF"])
                    });
                }
                return pluList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取商品图片
        /// </summary>
        /// <returns></returns>
        public static byte[] GetPluImage(decimal pluID,string ConnectionString)
        {
            try
            {
                using (OracleDAL dal = new OracleDAL(ConnectionString))
                {
                    dal.Open();
                    StringBuilder sql = new StringBuilder(128);
                    sql.AppendFormat("select pluImage from tskupluimage where pluID={0}",pluID);
                    OracleDataReader odr = dal.Select(sql.ToString());
                    byte[] img=null;
                    if (odr.Read())
                    {
                        img = (byte[])odr["pluImage"];
                    }
                    odr.Close();
                    return img;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
