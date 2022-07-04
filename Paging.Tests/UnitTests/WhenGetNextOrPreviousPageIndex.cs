namespace Paging.Tests.UnitTests
{
    public class WhenGetNextOrPreviousPageIndex
    {
        [Fact]
        public void GetIndexOfNextPageCurrentPageInTheMiddle_ReturnsCorrect()
        {
            var pageIndex = 6;
            var page = new PagedList<string>
            {
                PageIndex = pageIndex,
                TotalCount = 400,
                PageSize = 20,
                TotalPages = 20
            };

            var nextPageIndex = page.GetIndexOfNextPage();

            nextPageIndex.Should().Be(pageIndex + 1);
        }

        [Fact]
        public void GetIndexOfNextPageCurrentPageIsLast_ReturnsCorrect()
        {
            var pageIndex = 20;
            var page = new PagedList<string>
            {
                PageIndex = pageIndex,
                TotalCount = 400,
                PageSize = pageIndex,
                TotalPages = 20
            };

            var nextPageIndex = page.GetIndexOfNextPage();

            nextPageIndex.Should().BeNull();
        }

        [Fact]
        public void GetIndexOfPreviousPageCurrentPageInTheMiddle_ReturnsCorrect()
        {
            var pageIndex = 6;
            var page = new PagedList<string>
            {
                PageIndex = pageIndex,
                TotalCount = 400,
                PageSize = 20,
                TotalPages = 20
            };

            var nextPageIndex = page.GetIndexOfPreviousPage();

            nextPageIndex.Should().Be(pageIndex - 1);
        }

        [Fact]
        public void GetIndexOfPreviousPageCurrentPageIsFirst_ReturnsCorrect()
        {
            var pageIndex = 1;
            var page = new PagedList<string>
            {
                PageIndex = pageIndex,
                TotalCount = 400,
                PageSize = 20,
                TotalPages = 20
            };

            var nextPageIndex = page.GetIndexOfPreviousPage();

            nextPageIndex.Should().BeNull();
        }
    }
}
