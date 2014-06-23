using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data.OracleClient;
using Model;

namespace BLL
{
    public class HandCardBLL
    {

        /// <summary>
        /// 增加手牌号
        /// </summary>
        /// <returns></returns>
        public static bool AddHandCard(PadHandCard handCard, string connectionString)
        {
            using (OracleDAL dal = new OracleDAL(connectionString))
            {
                OracleTransaction tran = null;
                try
                {
                    dal.Open();
                    tran = dal.Connection.BeginTransaction();
                    StringBuilder sqlStr = new StringBuilder(256);
                    sqlStr.Append("insert into Tpadhandcard (Handcard,Barcode,Cardstatus,Shpid,Remark) Values (");
                    sqlStr.AppendFormat("'{0}','{1}','{2}',{3},'{4}')", handCard.HandCard, handCard.BarCode,handCard.CardStatus, handCard.ShpId, handCard.Remark);
                    int i;
                    dal.Execute(sqlStr.ToString(), tran, out i);
                    tran.Commit();
                    return i>0;
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
        /// 删除手牌号
        /// </summary>
        /// <param name="handCard"></param>
        /// <param name="shpId"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool DelHandCard(string handCard, string shpId, string connectionString,out string msg)
        {
            using (OracleDAL dal = new OracleDAL(connectionString))
            {
                OracleTransaction tran = null;
                try
                {
                    dal.Open();
                    tran = dal.Connection.BeginTransaction();
                    StringBuilder sqlStr = new StringBuilder(256);
                    sqlStr.AppendFormat("select cardstatus from tpadhandcard where handcard='{0}' and shpId={1} ", handCard, shpId);
                    OracleDataReader odr = dal.Select(sqlStr.ToString(), tran);
                    if (odr.Read())
                    {
                        if (Convert.ToString(odr["cardstatus"]) == "1")
                        {
                            //已占用
                            odr.Close();
                            tran.Rollback();
                            msg = "该手牌正在使用";
                            return false;
                        }
                        odr.Close();
                        sqlStr.Clear();
                        sqlStr.AppendFormat("Delete from tPadHandCard where handCard='{0}' and shpId={1}",
                                handCard, shpId
                            );
                        int i;
                        dal.Execute(sqlStr.ToString(), tran, out i);
                        tran.Commit();
                        msg = "删除成功";
                        return true;
                    }
                    else
                    {
                        odr.Close();
                        tran.Rollback();
                        msg = "该手牌不存在";
                        return false;
                    }
                   
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
        /// 列表手牌号
        /// </summary>
        /// <param name="shpId"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static ICollection<PadHandCard> ListHandCard(string shpId, string connectionString)
        {
            using (OracleDAL dal = new OracleDAL(connectionString))
            {
                try
                {
                    dal.Open();
                    ICollection<PadHandCard> handCards = new List<PadHandCard>();
                    StringBuilder sqlStr = new StringBuilder(256);
                    sqlStr.AppendFormat("Select * from tPadHandCard where shpId={0} Order by HandCard", shpId);
                    OracleDataReader odr = dal.Select(sqlStr.ToString());
                    while (odr.Read())
                    {
                        handCards.Add(new PadHandCard()
                        {
                            HandCard=Convert.ToString(odr["HandCard"]),
                            BarCode=Convert.ToString(odr["Barcode"]),
                            CardStatus=Convert.ToString(odr["CardStatus"]),
                            ShpId=Convert.ToString(odr["ShpId"]),
                            Remark=Convert.ToString(odr["Remark"])
                        });
                    }
                    odr.Close();
                    return handCards;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
