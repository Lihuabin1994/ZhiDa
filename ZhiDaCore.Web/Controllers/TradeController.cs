using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZhiDaCore.Appliaction;
using ZhiDaCore.Common;
using ZhiDaCore.Entity.Trades;
using ZhiDaCore.IService;
using ZhiDaCore.Web.Filter;

namespace ZhiDaCore.Web.Controllers
{
    public class TradeController : Controller
    {
        private readonly ITradeService _tradeSevice;
      
        public TradeController(ITradeService tradeSevice)
        {
            _tradeSevice = tradeSevice;
        }
       
        public IActionResult Index()
        {
            return View();
        }
       
        public async Task<IActionResult> Data(TradeModel data, int? page)
        {
            DateTime st;
            Logger.Info("GetTrades method start:" + DateTime.Now);
            st = DateTime.Now;
            var list = await _tradeSevice.GetTradesPages(data, page);
            Logger.Info("GetTrades method waist time:" + DateTime.Now.Subtract(st).TotalSeconds);
            return PartialView(list);
        }
      
        public async Task<IActionResult> ExportExcel(TradeModel entity)
        {
            string filename = "SGXTrades_" + DateTime.Now.ToString("yyyy-MM-dd") + $".xlsx";
            var list = await _tradeSevice.GetTrades(entity);
            list.ForEach(x => { x.SecType = (x.SecType == "FUT" ? "Future" : "Option"); x.Side = (x.Side == "1" ? "Buy" : "Sell"); });
            var columns = new Dictionary<string, string>() {
                                                             { "BizDate","Clearing Date"},
                                                             { "TradeDate","Trade Date"},
                                                             { "OrderID","OrderID"},
                                                             { "Exchange","Exchange"},
                                                             { "AccountNo","AccountNo"},
                                                             { "BizUnit","BizUnit"},
                                                             { "ProductCode","ProductCode"},
                                                             { "MMY","MMY"},
                                                              { "SecType","Security Type"},
                                                             { "PutCall","PutCall"},
                                                             { "StrikePx","StrikePrice"},
                                                             { "Side","Side"},
                                                             { "Qty","Quantity"},
                                                             { "TradePrice","TradePrice"},
                                                             { "Ccy","Currency"}
                                                            
                                                           };
            var fs = ExcelHelper.GetByteToExportExcel(list, columns, new List<string>(), "Trades", "", 0);
            return File(fs, "application/vnd.android.package-archive",filename );

        }

    }
}