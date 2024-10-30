namespace TestDelivery.Models;

public class District
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public District(long id, string name)
    {
        Id = id;
        Name = name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is District dist)
        {
            return Id == dist.Id;
        }

        return false;
    }
    
    public override string ToString()
    {
        return $"Id:{Id}, Name:{Name}";
    }
}