namespace Paging.Tests.UnitTests
{
    public class WhenGetPagedList
    {
        [Theory]
        [InlineData(47, 4, 5, 5, 47, 10)]
        [InlineData(47, 1, 5, 5, 47, 10)]
        [InlineData(47, 10, 5, 2, 47, 10)]
        [InlineData(47, 40, 5, 0, 47, 10)]
        [InlineData(0, 4, 5, 0, 0, 0)]
        public void ToPagedListWithValidParameters_ReturnsCorrect(int totalCount, int pageIndex, int pageSize, int expectedItemsCount, int expectedTotalCount, int expectedTotalPages)
        {
            var vehicles = GenerateListOfVehicles(totalCount);

            var PagedList = vehicles.AsQueryable().ToPagedList((v) => v.Id, pageIndex, pageSize);

            PagedList
                .Should()
                .BeOfType<PagedList<Vehicle>>();

            PagedList.PageSize.Should().Be(pageSize);
            PagedList.PageIndex.Should().Be(pageIndex);
            PagedList.TotalPages.Should().Be(expectedTotalPages);
            PagedList.TotalCount.Should().Be(expectedTotalCount);
            PagedList.Items.Count.Should().Be(expectedItemsCount);
        }

        [Theory]
        [InlineData(47, -4, 5)]
        [InlineData(47, 0, 5)]
        [InlineData(47, 4, -5)]
        [InlineData(47, 4, 0)]
        public void ToPagedListWithIncorrectParameters_ThrowsArgumentOutOfRangeException(int totalCount, int pageIndex, int pageSize)
        {
            var vehicles = GenerateListOfVehicles(totalCount);

            Action action = () => vehicles.AsQueryable().ToPagedList((v) => v.Id, pageIndex, pageSize);

            action.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        private List<Vehicle> GenerateListOfVehicles(int number)
        {
            var vehicles = new List<Vehicle>();
            for (int i = 0; i < number; i++)
            {
                vehicles.Add(new Vehicle(i, $"test {i}"));
            }
            return vehicles;
        }

        private class Vehicle
        {
            public Vehicle(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
