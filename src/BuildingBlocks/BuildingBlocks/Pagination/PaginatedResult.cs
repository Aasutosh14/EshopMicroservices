using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Pagination
{
    public class PaginatedResult<TEntity>(int PageIndex, int PageSize, long Count, IEnumerable<TEntity> Items) where TEntity : class
    {
        public int PageIndex { get; } = PageIndex;
        public int PageSize { get; } = PageSize;
        public IEnumerable<TEntity> Items { get; } = Items;
        public long Count { get; } = Count;
    }
}
