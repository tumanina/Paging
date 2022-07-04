namespace Paging.Tests.UnitTests
{
    public class WhenConvertPagedLists
    {
        [Fact]
        public void Convert_ReturnsCorrect()
        {
            var vehicles = GenerateListOfVehicles(20);

            var convertedList = new PagedList<Vehicle>
            {
                PageIndex = 2,
                TotalCount = 20,
                PageSize = 5,
                TotalPages = 4,
                Items = GenerateListOfVehicles(5)
            }.Convert(Map);

            convertedList.PageSize.Should().Be(5);
            convertedList.PageIndex.Should().Be(2);
            convertedList.TotalPages.Should().Be(4);
            convertedList.TotalCount.Should().Be(20);
            convertedList.Items.Count.Should().Be(5);
            convertedList.Items.Should().AllBeOfType<VehicleResponseModel>();
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

        private class VehicleResponseModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private VehicleResponseModel Map(Vehicle vehicle)
        {
            return new VehicleResponseModel { Id = vehicle.Id, Name = vehicle.Name };
        }
    }
}
