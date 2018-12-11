using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZhiDaCore.Common;
using ZhiDaCore.Entity.Trades;

namespace ZhiDaCore.IService
{
   public interface ITradeService
    {
       /// <summary>
       /// 获取交易分页数据
       /// </summary>
       /// <param name="model"></param>
       /// <param name="page"></param>
       /// <returns></returns>
        Task<PaginatedList<Trade>> GetTradesPages(TradeModel model,int?page);
        /// <summary>
        /// 获取导出excel的数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<Trade>> GetTrades(TradeModel model);
    }
}
