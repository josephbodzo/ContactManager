using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactManager.Infrastructure.Services.Utilities;

namespace ContactManager.Infrastructure.Services.Paging
{
    public static class PaginationExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, PagingOptions options)
        {
            if(options.Take == default)
            {
                options.Take = Constants.Constants.ConstantMaxPageTake;
            }

            int index = options.Skip / options.Take;
            return new PagedList<T>(source, index, options.Take);
        }
    }
}
