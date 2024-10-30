namespace TestDelivery.Models;

public class Order
{
    public long Id { get; set; }
    public double Weight { get; set; }
    public District District { get; set; }
    public DateTime TimeDelivery { get; set; }

    public Order(long id, double weight, District district, DateTime timeDelivery)
    {
        Id = id;
        Weight = weight;
        District = district;
        TimeDelivery = timeDelivery;
    }
    
    public override string ToString()
    {
        return $"Id:{Id}, Weight:{Weight}, District:{District}, TimeDelivery{TimeDelivery:O}";
    }
}