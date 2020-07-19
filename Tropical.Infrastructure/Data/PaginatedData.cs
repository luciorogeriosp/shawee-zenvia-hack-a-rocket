using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tropical.Infrastructure.Data
{
    public class PaginatedData<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedData(IQueryable<T> source, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();

            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            this.AddRange(source.Take(pageSize).Skip(PageIndex * PageSize));
        }

        public PaginatedData(IList<T> lista, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = lista.Count();

            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            int take = PageSize;

            if (PageIndex == TotalPages && (lista.Count() % 10) != 0)
            {
                take = (lista.Count() % 10);
            }

            this.AddRange(lista.Skip((PageIndex - 1) * PageSize).Take(take));
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex + 1 < TotalPages);
            }
        }
    }
}
