using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Paging.Tests.UnitTests
{
    public class WhenGetPagedListAsync
    {
        [Theory]
        [InlineData(47, 4, 5, 5, 47, 10)]
        [InlineData(47, 1, 5, 5, 47, 10)]
        [InlineData(47, 10, 5, 2, 47, 10)]
        [InlineData(47, 40, 5, 0, 47, 10)]
        public void ToPagedListAsyncWithValidParameters_ReturnsCorrect(int totalCount, int pageIndex, int pageSize, int expectedItemsCount, int expectedTotalCount, int expectedTotalPages)
        {
            var options = new DbContextOptionsBuilder<TestContext>().UseInMemoryDatabase(databaseName: "Test");
            using (var context = new TestContext(options.Options))
            {
                FillTestData(totalCount, context);

                var PagedList = context.Vehicles.ToPagedListAsync((v) => v.Id, pageIndex, pageSize).Result;

                PagedList
                    .Should()
                    .BeOfType<PagedList<VehicleEntity>>();

                PagedList.PageSize.Should().Be(pageSize);
                PagedList.PageIndex.Should().Be(pageIndex);
                PagedList.TotalPages.Should().Be(expectedTotalPages);
                PagedList.TotalCount.Should().Be(expectedTotalCount);
                PagedList.Items.Count.Should().Be(expectedItemsCount);
            }
        }

        [Theory]
        [InlineData(47, -4, 5)]
        [InlineData(47, 0, 5)]
        [InlineData(47, 4, -5)]
        [InlineData(47, 4, 0)]
        public void ToPagedListWithIncorrectParameters_ThrowsArgumentOutOfRangeException(int totalCount, int pageIndex, int pageSize)
        {
            var options = new DbContextOptionsBuilder<TestContext>().UseInMemoryDatabase(databaseName: "Test");
            using (var context = new TestContext(options.Options))
            {
                FillTestData(totalCount, context);

                Action action = () => context.Vehicles.ToPagedListAsync((v) => v.Id, pageIndex, pageSize).GetAwaiter().GetResult();

                action.Should().ThrowExactly<ArgumentOutOfRangeException>();
            }
        }

        private void FillTestData(int number, TestContext context)
        {
            if (!context.Vehicles.Any())
            {
                for (int i = 1; i <= number; i++)
                {
                    var vehicle = new VehicleEntity(i, $"test {i}");
                    context.Vehicles.Add(vehicle);
                }
                context.SaveChanges();
            }
        }

        private class VehicleEntity
        {
            public VehicleEntity(int id, string name)
            {
                Id = id;
                Name = name;
            }

            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private class TestContext : DbContext
        {
            public TestContext(DbContextOptions<TestContext> options) : base(options) { }

            public virtual DbSet<VehicleEntity> Vehicles { get; set; }
        }
    }

}
