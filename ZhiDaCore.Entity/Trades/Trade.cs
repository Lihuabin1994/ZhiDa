using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZhiDaCore.Entity.Trades
{
    public class Trade
    {
        #region Model		
        public int SequenceID { get; set; }
        public string BizUnit { get; set; }
        public string Exchange { get; set; }
        public string AccountNo { get; set; }
        public string TradeId { get; set; }
        [Display(Name = "Currency")]
        public string Ccy { get; set; }
        public string Side { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal? TradePrice { get; set; }
        public string EventType { get; set; }
        public string EventDesc { get; set; }
        [Display(Name = "Quantity")]
        public int Qty { get; set; }
        public string MMY { get; set; }
        public string ProductCode { get; set; }
        [Display(Name = "Security Type")]
        public string SecType { get; set; }
        public string PutCall { get; set; }
        [Display(Name = "StrikePrice")]
        public decimal? StrikePx { get; set; }
        public string StrikePrice { get; set; }
        [Display(Name = "Trade Date")]
        public DateTime TradeDate { get; set; }
        public string BusinessDate { get; set; }
        public string Multiplier { get; set; }
        public string TradingUser { get; set; }
        [Display(Name = "Clearing Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? BizDate { get; set; }
        public DateTime? InsertDate { get; set; }
        public string ClearingMember { get; set; }
        public string TradingMember { get; set; }
        public string AccountType { get; set; }
        public DateTime? TransTime { get; set; }
        public int ContractSize { get; set; }
        public string Market { get; set; }
        public string GroupId { get; set; }
        public string OrderID { get; set; }
        #endregion Model
    }
}

