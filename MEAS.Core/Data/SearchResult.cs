using System;
using System.Collections.Generic;

namespace MEAS.Data
{
    public sealed class SearchResult<T>
        where T : class
    {
        public SearchResult(IEnumerable<T> data, int totalCount)
        {
            this.Data = data;
            TotalCount = totalCount;
        }

        public int TotalCount { get; private set; }

        public IEnumerable<T> Data { get; private set; }
    }
}
