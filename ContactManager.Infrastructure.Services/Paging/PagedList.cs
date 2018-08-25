using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace ContactManager.Infrastructure.Services.Paging
{
    public class PagedList<T> : List<T>
    {
        private readonly int _totalCount;
        public bool ReturnedAllRecords =>  PageIndex == PageCount - 1;
        public int PageIndex { get; }
        public int PageSize { get; }
        public int PageCount => _totalCount / PageSize + (_totalCount % PageSize == 0 ? 0 : 1);

        private PagedList(int pageIndex, int pageSize, int totalCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            _totalCount = totalCount;
        }

        public PagedList(IQueryable<T> source, int index, int pageSize)
        {
           _totalCount = source.Count();
           PageIndex = index;
           PageSize = pageSize;

           source = source.Skip(index * pageSize).Take(pageSize);

           AddRange(source.ToList());
        }

        public PagedList<TDestination> Map<TDestination> (Func<T, TDestination> mappingFunc)
        {
            var newPagedList = new PagedList<TDestination>(PageIndex, PageSize, _totalCount);
            newPagedList.AddRange(this.Select(mappingFunc));
            return newPagedList;
        }
    }
}