using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XiechengTravel.Helper
{
    /// <summary>
    /// 分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginationList<T>:List<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;//是否有上一页
        public bool HasNext => CurrentPage < TotalPages;//是否有下一页

        public PaginationList(int CurrentPage,int TotalCount, int PageSize,IList<T> list)
        {
            this.CurrentPage = CurrentPage;
            this.PageSize = PageSize;
            this.TotalCount = TotalCount;
            this.TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            AddRange(list);
        }
        /// <summary>
        /// 工厂设计模式
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static async Task<PaginationList<T>> createPageAsync(
            int CurrentPage, int PageSize,IQueryable<T> result
            )
        {
            var totalCount = await result.CountAsync();
            var re = await result.Skip(((CurrentPage - 1) * PageSize)).Take(PageSize).ToListAsync();
            return new PaginationList<T>(CurrentPage,totalCount, PageSize, re);
        }
    }
}
