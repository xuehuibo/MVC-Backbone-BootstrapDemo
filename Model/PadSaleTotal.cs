using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PadSaleTotal
    {
        public int InvoiceCount
        {
            get;
            set;
        }

        public decimal InvoiceMoney
        {
            get;
            set;
        }

        public int TookGoodsCount
        {
            get;
            set;
        }

        public decimal TookGoodsMoney
        {
            get;
            set;
        }

        public int CancelCount
        {
            get;
            set;
        }
        public decimal CancelMoney
        {
            get;
            set;
        }
    }
}
