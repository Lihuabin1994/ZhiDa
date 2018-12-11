using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiDaCore.Common
{
    public class PaginatedList<T> : List<T>
    {

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// 总共页数
        /// </summary>
        public int TotalPages { get; private set; }
        public int? SellNum { get; private set; }
        public int? BuyNum { get; private set; }
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, int? sellNum, int? buyNum)
        {
            SellNum = sellNum;
            BuyNum = buyNum;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
          
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize,int? SellNum,int? BuyNum)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize,SellNum,BuyNum);
        }
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize, 0, 0);
        }

    }
}
