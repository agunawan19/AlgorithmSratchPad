using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLibrary.PagingService
{
    public class PagingService : IPagingService
    {
        public int TotalItems { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int CurrentPage { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int ItemStartIndex { get; private set; }
        public int ItemEndIndex { get; private set; }
        public IEnumerable<int> Pages { get; private set; }

        public PagingService(int totalItems, int pageSize = 10, int currentPage = 1, int pageCount = 10) => 
            Calculate(totalItems, pageSize, currentPage, pageCount);

        public void Calculate(int totalItems, int pageSize = 10, int currentPage = 1, int pageCount = 10)
        {
            const string argumentExceptionMessage = "Value must be greater than or equal 1";

            TotalItems = totalItems > -1 ? totalItems : 0;
            PageSize = pageSize >= 1 ? pageSize : 10;
            pageCount = pageCount >= 1 ? pageCount : 1;
            
            TotalPages = CalculateTotalPages();
            CurrentPage = currentPage > TotalPages ? TotalPages : currentPage;
            (ItemStartIndex, ItemEndIndex) = CalculateStartIndexAndEndIndex();
            (StartPage, EndPage) = CalculateStartPageAndEndPage(pageCount);
            Pages = GeneratePages();
        }

        private int CalculateTotalPages()
        {
            var totalPage = (int)Math.Ceiling((decimal)TotalItems / PageSize);
            
            return totalPage == 0 ? 1 : totalPage;
        }

        private (int, int) CalculateStartIndexAndEndIndex()
        {
            var startIndex = (CurrentPage - 1) * PageSize;
            var endIndex = Math.Min(startIndex + PageSize - 1, TotalItems - 1);
            
            return (startIndex, endIndex);
        }        

        private (int, int) CalculateStartPageAndEndPage(int pageCount)
        {
            int startPage;
            int endPage;

            if (TotalPages <= pageCount)
            {
                startPage = 1;
                endPage = pageCount;
            }
            else
            {
                var maxPagesBeforeCurrentPage = (int)Math.Floor((decimal)pageCount / 2);
                var maxPagesAfterCurrentPage = (int)Math.Ceiling((decimal)pageCount / 2) - 1;

                switch (CurrentPage)
                {
                    case var currentPage when currentPage <= maxPagesBeforeCurrentPage:
                        startPage = 1;
                        endPage = pageCount;
                        break;
                    case var currentPage when currentPage + maxPagesAfterCurrentPage >= TotalPages:
                        startPage = TotalPages - pageCount + 1;
                        endPage = TotalPages;
                        break;
                    default:
                        startPage = CurrentPage - maxPagesBeforeCurrentPage;
                        endPage = CurrentPage + maxPagesAfterCurrentPage;                        
                        break;
                }
            }

            return (startPage, endPage);
        }
        
        private IEnumerable<int> GeneratePages() => Enumerable.Range(StartPage, (EndPage + 1) - StartPage);
    }
}
