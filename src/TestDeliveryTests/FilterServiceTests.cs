using TestDelivery.Models;
using TestDelivery.Services;

namespace TestDeliveryTests;

public class FilterServiceTests
{
    private readonly FilterService _filterService;
    
    public FilterServiceTests()
    {
        _filterService = new FilterService();
    }

    [Fact]
    public void FilterOrders_ValidInput_ReturnsFilteredOrders()
    {
        var orders = new List<Order>
        {
            new Order(1, 10.5, new District(1, "Center"), DateTime.Now.AddMinutes(5)),
            new Order(2, 15.0, new District(1, "Center"), DateTime.Now.AddMinutes(10)),
            new Order(3, 7.2, new District(3, "South"), DateTime.Now.AddMinutes(15))
        };

        var cityDistrict = new District(1, "Center");
        var firstDeliveryDateTime = DateTime.Now;

        var result = _filterService.OrdersForDeliveryWithinNextMinutes(orders, cityDistrict, firstDeliveryDateTime,
            new DateTime(firstDeliveryDateTime.Ticks).AddMinutes(30));

        Assert.True(result.Count == 2);
        Assert.Equal(1, result[0].Id);
    }

    [Fact]
    public void FilterOrders_NoMatchingOrders_ReturnsEmptyList()
    {
        var orders = new List<Order>
        {
            new Order(1, 10, new District(0, "North"), DateTime.Now.AddMinutes(10))
        };

        var cityDistrict = new District(1, "Center");
        var firstDeliveryDateTime = DateTime.Now;

        var result = _filterService.OrdersForDeliveryWithinNextMinutes(orders, cityDistrict, firstDeliveryDateTime,
            new DateTime(firstDeliveryDateTime.Ticks).AddMinutes(30));

        Assert.Empty(result);
    }
}