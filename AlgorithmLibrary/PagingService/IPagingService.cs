using System.Collections.Generic;

namespace AlgorithmLibrary.PagingService
{
    public interface IPagingService
    {
        public int TotalItems { get;  }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int CurrentPage { get; }
        public int ItemStartIndex { get; }
        public int ItemEndIndex { get; }
        public int StartPage { get; }
        public int EndPage { get; }
        public IEnumerable<int> Pages { get; }
        void Calculate(int totalItems, int pageSize = 10, int currentPage = 1, int pageCount = 10);
    }
}
