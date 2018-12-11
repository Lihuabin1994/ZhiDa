using System;
using System.Collections.Generic;
using System.Text;

namespace ZhiDaCore.Entity.Trades
{
    public class TradeModel
    {
        public string Exchange { get; set; }
        public string AccountNo { get; set; }
        public string SecType { get; set; }
        public string MMY { get; set; }
        public string ProductCode { get; set; }
        public DateTime? BizDate { get; set; }
        public string OrderID { get; set; }
        public string Side { get; set; }
        public int PageSize { get; set; }
        //public DateTime? TradeDate { get; set; }
        //public string BizUnit { get; set; }
        //public string PutCall { get; set; }
        //public decimal? StrikePx { get; set; }
        //public int? Qty { get; set; }
        //public decimal? TradePrice { get; set; }
        //public string Ccy { get; set; }
    }
}
