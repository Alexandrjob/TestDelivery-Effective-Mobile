using Serilog;

namespace TestDelivery.Services;

using Models;

public class FilterService
{
    public List<Order> OrdersForDeliveryWithinNextMinutes(List<Order> orders, District dist, DateTime firstDelivery, DateTime time)
    {
        var result = orders
            .Where(o => Equals(o.District, dist))
            .Where(o => o.TimeDelivery <= time && o.TimeDelivery >= firstDelivery)
            .OrderBy(o => o.TimeDelivery)
            .ToList();
        
        Log.Information($"Filtering orders(count {result.Count}) by district id:{dist.Id}, first delivery time:{firstDelivery}, {time}");
        
        return result;
    }
}