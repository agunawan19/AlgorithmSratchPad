using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using AlgorithmLibrary.PagingService;

namespace AlgorithmLibrary.Tests.PagingService
{
    [TestClass]
    public class PagingServiceTests
    {
        [TestMethod]
        [DataRow(-100, 0)]
        [DataRow(0, 0)]
        [DataRow(100, 100)]
        public void TotalItems_Returns_ExpectedResult(int totalItems, int expectedTotalItems)
        {
            IPagingService pagingService = new AlgorithmLibrary.PagingService.PagingService(totalItems);

            pagingService.TotalItems.Should().Be(expectedTotalItems);
        }
        
        [TestMethod]
        [DataRow( 10, 10)]
        [DataRow( 5, 5)]
        [DataRow( 0, 10)]
        public void PageSize_Returns_ExpectedResult(int pageSize, int expectedPageSize)
        {
            IPagingService pagingService = new AlgorithmLibrary.PagingService.PagingService(100, pageSize);

            pagingService.PageSize.Should().Be(expectedPageSize);
        }
        
        // [TestMethod]
        // public void PageSize_Returns_Arguments_Exception_When_Page_Size_Is_Less_Than_One()
        // {
        //     Action action = () =>
        //     {
        //         var _ = new AlgorithmLibrary.PagingService.PagingService(10, 0);
        //     };
        //  
        //     action.Should()
        //         .Throw<ArgumentException>("Page size is less than 1")
        //         .Where(e => e.Message.StartsWith("Value must be greater than or equal 1"))
        //         .And.ParamName.Should().Be("pageSize");
        // }
        
        [TestMethod]
        [DataRow(1, 10, 1)]
        [DataRow(10, 10, 1)]
        [DataRow(11, 10, 2)]
        [DataRow(100, 10, 10)]
        [DataRow(0, 10, 1)]
        [DataRow(-100, 10, 1)]
        public void TotalPages_Returns_ExpectedResult(int totalItems, int pageSize, int expectedTotalPages)
        {
            IPagingService pagingService = new AlgorithmLibrary.PagingService.PagingService(totalItems, pageSize);

            pagingService.TotalPages.Should().Be(expectedTotalPages);
        }

        [TestMethod]
        [DataRow(10, 10, 2, 1)]
        [DataRow(100, 10, 11, 10)]
        public void CurrentPage_Returns_ExpectedValue_When_CurrentPage_IsGreater_Than_TotalPages(int totalItems, 
            int pageSize, int currentPage, int expectedCurrentPage)
        {
            IPagingService pagingService = new AlgorithmLibrary.PagingService.PagingService(totalItems, pageSize, 
                currentPage);

            pagingService.CurrentPage.Should().Be(expectedCurrentPage);
        }
        
        [TestMethod]
        public void TestPlaceHolder()
        {
            // IPagingService pagingService = new AlgorithmLibrary.PagingService.PagingService(150, 15, 7, 5);
            //
            // pagingService.TotalItems.Should().Be(150);
            // pagingService.PageSize.Should().Be(15);
            // pagingService.CurrentPage.Should().Be(7);
            // pagingService.StartPage.Should().Be(5);
            // pagingService.EndPage.Should().Be(9);
            // pagingService.ItemStartIndex.Should().Be(90);
            // pagingService.ItemEndIndex.Should().Be(104);
            // pagingService.Pages.Should().BeEquivalentTo(new[] {5, 6, 7, 8, 9});
            
            IPagingService pagingService = new AlgorithmLibrary.PagingService.PagingService(100, 10, 9, 5);
            
            pagingService.TotalItems.Should().Be(100);
            pagingService.PageSize.Should().Be(10);
            pagingService.CurrentPage.Should().Be(9);
            // pagingService.StartPage.Should().Be(3);
            // pagingService.EndPage.Should().Be(7);
            // pagingService.ItemStartIndex.Should().Be(40);
            // pagingService.ItemEndIndex.Should().Be(49);
            // pagingService.Pages.Should().BeEquivalentTo(new[] {5, 6, 7, 8, 9});
        }
    }
}
