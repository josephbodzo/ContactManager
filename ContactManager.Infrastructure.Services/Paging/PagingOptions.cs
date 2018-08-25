using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManager.Infrastructure.Services.Paging
{
   public class PagingOptions
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
