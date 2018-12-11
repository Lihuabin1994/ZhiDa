using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZhiDaCore.Common;
using ZhiDaCore.EFCore;
using ZhiDaCore.Entity.Trades;
using ZhiDaCore.IService;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ZhiDaCore.Service
{
    public class TradeService : ITradeService
    {
        private readonly DataContext _context;
        public TradeService(DataContext context)
        {
            _context = context;
        }
        public async Task<PaginatedList<Trade>> GetTradesPages(TradeModel data, int? page)
        {
            var condition = ConditionBuilder.True<Trade>();
            if (!string.IsNullOrEmpty(data.Exchange)) { condition = condition.And(x=>x.Exchange.Contains(data.Exchange)); }
            if (!string.IsNullOrEmpty(data.AccountNo)) {condition = condition.And(x=>x.AccountNo.Contains(data.AccountNo));}
            if (!string.IsNullOrEmpty(data.SecType)) { condition = condition.And(x => x.SecType == data.SecType); };
            if (!string.IsNullOrEmpty(data.MMY)) { condition = condition.And(x => x.MMY.Contains(data.MMY)); }
            if (!string.IsNullOrEmpty(data.ProductCode)) { condition = condition.And(x => x.ProductCode.Contains(data.ProductCode)); }
            if (!string.IsNullOrEmpty(data.BizDate.ToString())) { condition = condition.And(x => x.BizDate==data.BizDate); }
            if (!string.IsNullOrEmpty(data.OrderID)) { condition = condition.And(x => x.OrderID.Contains(data.OrderID)); };
            if (!string.IsNullOrEmpty(data.Side)) { condition = condition.And(x => x.Side.Contains(data.Side)); };

            var trade = from s in _context.Trade.Where(condition) select s;

                //if (!string.IsNullOrEmpty(data.Exchange)) { trade = trade.Where(x => x.Exchange.Contains(data.Exchange)); }
                //if (!string.IsNullOrEmpty(data.AccountNo)) { trade = trade.Where(x => x.AccountNo.Contains(data.AccountNo)); }
                //if (!string.IsNullOrEmpty(data.SecType)) { trade = trade.Where(x => x.SecType.Contains(data.SecType)); }
                //if (!string.IsNullOrEmpty(data.MMY)) { trade = trade.Where(x => x.MMY.Contains(data.MMY)); }
                //if (!string.IsNullOrEmpty(data.ProductCode)) { trade = trade.Where(x => x.ProductCode.Contains(data.ProductCode)); }
                //if (!string.IsNullOrEmpty(data.BizDate.ToString())) { trade = trade.Where(x => x.BizDate.ToString().Contains(data.BizDate.ToString())); }
                //if (!string.IsNullOrEmpty(data.OrderID)) { trade = trade.Where(x => x.OrderID.Contains(data.OrderID)); };
                //if (!string.IsNullOrEmpty(data.Side)) { trade = trade.Where(x => x.Side == data.Side); };


            int sell =await trade.AsNoTracking().Where(x=>x.Side=="2").SumAsync(x => x.Qty);
            int buy = await trade.AsNoTracking().Where(x => x.Side == "1").SumAsync(x => x.Qty);
            int pageSize = data.PageSize==0?15:data.PageSize;
         

            return await PaginatedList<Trade>.CreateAsync(trade.AsNoTracking().OrderBy(x=>x.BizDate), page ?? 1, pageSize,sell,buy);

        }
        public async Task<List<Trade>> GetTrades(TradeModel data)
        {
            var condition = ConditionBuilder.True<Trade>();
            if (!string.IsNullOrEmpty(data.Exchange)) { condition = condition.And(x => x.Exchange.Contains(data.Exchange)); }
            if (!string.IsNullOrEmpty(data.AccountNo)) { condition = condition.And(x => x.AccountNo.Contains(data.AccountNo)); }
            if (!string.IsNullOrEmpty(data.SecType)) { condition = condition.And(x => x.SecType == data.SecType); };
            if (!string.IsNullOrEmpty(data.MMY)) { condition = condition.And(x => x.MMY.Contains(data.MMY)); }
            if (!string.IsNullOrEmpty(data.ProductCode)) { condition = condition.And(x => x.ProductCode.Contains(data.ProductCode)); }
            if (!string.IsNullOrEmpty(data.BizDate.ToString())) { condition = condition.And(x => x.BizDate == data.BizDate); }
            if (!string.IsNullOrEmpty(data.OrderID)) { condition = condition.And(x => x.OrderID.Contains(data.OrderID)); };
            if (!string.IsNullOrEmpty(data.Side)) { condition = condition.And(x => x.Side.Contains(data.Side)); };

            var trade = from s in _context.Trade.Where(condition) select s;
            return await trade.AsNoTracking().OrderBy(x=>x.BizDate).ToListAsync();
        }
    }
}
