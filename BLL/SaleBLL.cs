using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data;

namespace BLL
{
    public class SaleBLL
    {
        /// <summary>
        /// 开单
        /// </summary>
        /// <param name="padSale"></param>
        /// <param name="padSalePlus"></param>
        /// <returns></returns>
        public static string DoInvoice(PadSale padSale, UserModel user, string connectionString)
        {
            using (OracleDAL dal = new OracleDAL(connectionString))
            {
                OracleTransaction tran = null;
                try
                {
                    dal.Open();
                    tran = dal.Connection.BeginTransaction();

                    decimal padSaleNo = 0;
                    OracleDataReader rd = dal.Select("select FPad_PadSaleNo from dual", tran);
                    if (rd.Read())
                    {
                        padSaleNo = Convert.ToDecimal(rd[0]);
                    }
                    rd.Close();

                    StringBuilder sql = new StringBuilder(256);
                    sql.Append(" Insert Into tPadSale(PadSaleNo,OrgCode,ShpId,Shpcode,Shpname,Clerkid,Clerkcode,Clerkname,Vipcardno,Xstotal,Needpaytotal) ");
                    sql.Append(" Values ( ");
                    sql.AppendFormat(" {0},'{1}',{2},'{3}','{4}',{5},'{6}','{7}','{8}',{9},{10} ",
                        padSaleNo, user.OrgCode, user.ShopID, user.ShopCode, user.ShopName, user.UserID, user.UserCode, user.UserName, padSale.VipCardNo, padSale.XsTotal, padSale.NeedPayTotal);
                    sql.Append(" ) ");

                    DataTable dtSalePlu = new DataTable("tPadSalePlu");
                    dtSalePlu.Columns.Add("PadSaleNo", typeof(decimal));
                    dtSalePlu.Columns.Add("OrgCode", typeof(string));
                    dtSalePlu.Columns.Add("LnNo", typeof(decimal));
                    dtSalePlu.Columns.Add("PluId", typeof(decimal));
                    dtSalePlu.Columns.Add("PluCode", typeof(string));
                    dtSalePlu.Columns.Add("PluName", typeof(string));
                    dtSalePlu.Columns.Add("Price", typeof(decimal));
                    dtSalePlu.Columns.Add("HyPrice", typeof(decimal));
                    dtSalePlu.Columns.Add("FsPrice", typeof(decimal));
                    dtSalePlu.Columns.Add("XsCount", typeof(decimal));
                    dtSalePlu.Columns.Add("XsTotal", typeof(decimal));
                    dtSalePlu.Columns.Add("DisCountOff",typeof(decimal));
                    dtSalePlu.Columns.Add("Remark", typeof(string));
                    dtSalePlu.Columns.Add("Udp1", typeof(string));
                    dtSalePlu.Columns.Add("Udp2", typeof(string));
                    dtSalePlu.Columns.Add("Udp3", typeof(string));
                    
                    foreach (PadSalePlu padSalePlu in padSale.SalePlu)
                    {
                        DataRow newRow = dtSalePlu.NewRow();
                        newRow["PadSaleNo"] = padSaleNo;
                        newRow["OrgCode"] = user.OrgCode;
                        newRow["LnNo"] = padSalePlu.LnNo;
                        newRow["PluId"] = padSalePlu.PluId;
                        newRow["PluCode"] = padSalePlu.PluCode;
                        newRow["PluName"] = padSalePlu.PluName;
                        newRow["Price"] = padSalePlu.Price;
                        newRow["HyPrice"] = padSalePlu.HyPrice;
                        newRow["FsPrice"] = padSalePlu.FsPrice;
                        newRow["XsCount"] = padSalePlu.XsCount;
                        newRow["XsTotal"] = padSalePlu.XsTotal;
                        newRow["DisCountOff"] = padSalePlu.DisCountOff;
                        newRow["Remark"] = padSalePlu.Remark;
                        dtSalePlu.Rows.Add(newRow);
                    }

                    int i;
                    dal.Execute(sql.ToString(), tran, out i);
                    dal.Save(dtSalePlu, tran, out i);
                    tran.Commit();
                    return padSaleNo.ToString("F0");
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    throw;
                }
            }

        }

        /// <summary>
        /// 获取提货单列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="payStatus"></param>
        /// <param name="saleSource"></param>
        /// <param name="pageNo">从1开始</param>
        /// <param name="pageSize"></param>
        /// <param name="user"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IEnumerable<PadSale> GetTakeGoodsData(string code, string payStatus, string saleSource,int pageNo,int pageSize, UserModel user, string connectionString)
        {
            try
            {
                using (OracleDAL dal = new OracleDAL(connectionString))
                {
                    dal.Open();
                    int startIndex, endIndex;
                    startIndex = (pageNo-1) * pageSize+1;
                    endIndex = startIndex + pageSize-1;
                    List<OracleParameter> ps=new List<OracleParameter>();
                    StringBuilder strB = new StringBuilder(256);
                    strB.Append(" select * from ( ");
                    strB.Append("  select rownum as serialNo, D.* from ( ");
                    strB.Append(" select A.*,B.ENUMVALUENAME as payStatusName,C.ENUMVALUENAME as SaleSourceName ");
                    strB.Append(" from tPadSale A, ");
                    strB.Append(" (Select EnumValueId,EnumValueName From tSysEnumValue Where EnumTypeId=11020) B, ");
                    strB.Append(" (Select EnumValueId,EnumValueName From tSysEnumValue Where EnumTypeId=11021) C ");
                    strB.Append(" where A.paystatus=B.enumvalueid and A.salesource=C.enumvalueid ");
                    if (!string.IsNullOrEmpty(code))
                    {
                        strB.Append(" and ( padSaleNo=:code or vipCardNo=:code ) ");
                        OracleParameter p0 = new OracleParameter(":code", code);
                        ps.Add(p0);
                    }
                    strB.Append(" and orgCode=:orgCode ");
                    strB.Append(" and shpId=:shpId ");
                    strB.Append(" and IsActive='1' and IsTook='0' ");
                    if (!string.IsNullOrEmpty(payStatus))
                    {
                        OracleParameter p1 = new OracleParameter(":payStatus", int.Parse(payStatus));
                        strB.Append(" and payStatus=:payStatus ");
                        ps.Add(p1);
                    }
                    if (!string.IsNullOrEmpty(saleSource))
                    {
                        OracleParameter p2 = new OracleParameter(":saleSource", int.Parse(saleSource));
                        strB.Append(" and saleSource=:saleSource ");
                        ps.Add(p2);
                    }
                    strB.Append(" order by PayStatus desc, BuildDate desc ) D ) ");
                    strB.AppendFormat(" where SerialNo between {0} and {1}",startIndex,endIndex);
                    OracleParameter p3=new OracleParameter(":orgCode",user.OrgCode);
                    OracleParameter p4=new OracleParameter(":shpId",user.ShopID);
                    ps.Add(p3);
                    ps.Add(p4);
                    int i;
                    DataTable rst = dal.Select(strB.ToString(), out i, ps.ToArray());
                    if (i > 0)
                    {
                        List<PadSale> saleList = new List<PadSale>();
                        strB.Clear();
                        strB.AppendFormat("Select * from Tpadsaleplu where padSaleNo=:padSaleNo and orgCode={0} order by LnNo",
                            user.OrgCode
                            );
                        foreach (DataRow row in rst.Rows)
                        {
                            PadSale sale = new PadSale()
                            {
                                PadSaleNo = Convert.ToDecimal(row["PadSaleNo"]),      //开票唯一标识
                                OrgCode = Convert.ToString(row["OrgCode"]),  //组织号
                                ShpId = Convert.ToDecimal(row["ShpId"]),      //专柜ID
                                ShpCode = Convert.ToString(row["ShpCode"]),     //专柜CODE
                                ShpName = Convert.ToString(row["ShpName"]),
                                ClerkId = Convert.ToDecimal(row["ClerkId"]),      //营业员ID
                                ClerkCode = Convert.ToString(row["ClerkCode"]),     //营业员编码
                                ClerkName = Convert.ToString(row["ClerkName"]),
                                VipCardNo = Convert.ToString(row["VipCardNo"]),    //会员卡号
                                XsTotal = Convert.ToDecimal(row["XsTotal"]),     //总交易金额
                                NeedPayTotal = Convert.ToDecimal(row["NeedPayTotal"]),  //待支付金额
                                PayStatus = Convert.ToString(row["PayStatus"]), // 0-未支付   1-部分支付  2-已支付
                                PosPayTotal = Convert.ToDecimal(row["PosPayTotal"]),
                                PayStatusName = Convert.ToString(row["PayStatusName"]),
                                IsActive = Convert.ToString(row["IsActive"]),  //是否有效，为0的表示已经删除
                                IsTook = Convert.ToString(row["IsTook"]),  //是否已提货，0-未提，1-已提
                                SaleSource = Convert.ToString(row["SaleSource"]),  //0-Pad开票 其他预留
                                SaleSourceName = Convert.ToString(row["SaleSourceName"]),
                                SalePlu=new List<PadSalePlu>()
                            };
                            saleList.Add(sale);
                            OracleParameter padSaleNoParam = new OracleParameter(":padSaleNo", sale.PadSaleNo);
                            DataTable plu=dal.Select(strB.ToString(), out i, padSaleNoParam);
                            foreach (DataRow pluRow in plu.Rows)
                            {
                                sale.SalePlu.Add(new PadSalePlu()
                                {
                                    PadSaleNo = Convert.ToDecimal(pluRow["PadSaleNo"]),
                                    OrgCode = Convert.ToString(pluRow["OrgCode"]),
                                    LnNo = Convert.ToInt16(pluRow["LnNo"]),
                                    PluId = Convert.ToDecimal(pluRow["PluId"]),
                                    PluCode = Convert.ToString(pluRow["PluCode"]),
                                    PluName = Convert.ToString(pluRow["PluName"]),
                                    Price = Convert.ToDecimal(pluRow["Price"]),
                                    HyPrice = Convert.ToDecimal(pluRow["HyPrice"]),
                                    FsPrice = Convert.ToDecimal(pluRow["FsPrice"]),
                                    XsCount = Convert.ToDecimal(pluRow["XsCount"]),
                                    XsTotal = Convert.ToDecimal(pluRow["XsTotal"]),
                                    DisCountOff = Convert.ToDecimal(pluRow["DisCountOff"]),
                                    Remark = Convert.ToString(pluRow["Remark"])
                                });
                            }
                        }
                        return saleList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取支付状态枚举
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IEnumerable<EnumModel> GetPayStatus(string connectionString)
        {
            try
            {
                using (OracleDAL dal = new OracleDAL(connectionString))
                {
                    List<EnumModel> payStatus = new List<EnumModel>();
                    dal.Open();
                    OracleDataReader odr = dal.Select("Select EnumValueId,EnumValueName From tSysEnumValue Where EnumTypeId=11020");
                    while (odr.Read())
                    {
                        payStatus.Add(new EnumModel()
                        {
                            EnumValueId = Convert.ToInt16(odr["EnumValueId"]),
                            EnumValueName = Convert.ToString(odr["EnumValueName"])
                        });
                    }
                    odr.Close();
                    return payStatus;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取来源枚举
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<EnumModel> GetSaleSource(string connectionString)
        {
            try
            {
                using (OracleDAL dal = new OracleDAL(connectionString))
                {
                    List<EnumModel> payStatus = new List<EnumModel>();
                    dal.Open();
                    OracleDataReader odr = dal.Select("Select EnumValueId,EnumValueName From tSysEnumValue Where EnumTypeId=11021");
                    while (odr.Read())
                    {
                        payStatus.Add(new EnumModel()
                        {
                            EnumValueId = Convert.ToInt16(odr["EnumValueId"]),
                            EnumValueName = Convert.ToString(odr["EnumValueName"])
                        });
                    }
                    odr.Close();
                    return payStatus;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 提货
        /// </summary>
        /// <param name="padSaleNo"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool TakeGoods(string padSaleNo, UserModel user,string connectionString ,out string msg)
        {
            using (OracleDAL dal = new OracleDAL(connectionString))
            {
                OracleTransaction tran=null;
                try
                {
                    dal.Open();
                    tran = dal.Connection.BeginTransaction();
                    StringBuilder strb = new StringBuilder(256);
                    strb.AppendFormat(" select IsTook ,PayStatus,IsActive from tPadSale where PadSaleNo='{0}' and OrgCode='{1}' and Shpid='{2}'",
                        padSaleNo,user.OrgCode,user.ShopID);
                    OracleDataReader odr = dal.Select(strb.ToString(), tran);
                    string isTook, payStatus, isActive;
                    if (odr.Read())
                    {
                        isTook = Convert.ToString(odr["IsTook"]);
                        payStatus = Convert.ToString(odr["PayStatus"]);
                        isActive = Convert.ToString(odr["IsActive"]);
                    }
                    else
                    {
                        odr.Close();
                        tran.Rollback();
                        msg = "未找到该流水";
                        return false;
                    }
                    odr.Close();
                    if (isActive == "0")
                    {
                        msg = "流水[" + padSaleNo + "]已删除";
                        tran.Rollback();
                        return false;
                    }
                    if (isTook == "1")
                    {
                        msg = "不能重复提货，流水[" + padSaleNo + "]已提货";
                        tran.Rollback();
                        return false;
                    }
                    if (payStatus != "2")
                    {
                        tran.Rollback();
                        msg = "未完成付款，流水[" + padSaleNo + "]不允许提货";
                        return false;
                    }
                    strb.Clear();
                    strb.AppendFormat(" update Tpadsale set IsTook='1' where PadSaleNo='{0}' ", padSaleNo);
                    int i;
                    dal.Execute(strb.ToString(), tran, out i);
                    tran.Commit();
                    msg = "流水[" + padSaleNo + "]提货成功！";
                    return true;
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    throw;
                }
            }
        }
    }
}
