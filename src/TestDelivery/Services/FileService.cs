using System.Text.Json;
using Serilog;
using TestDelivery.Extensions;

namespace TestDelivery.Services;

using Models;

public class FileService
{
    public void WriteOrders(string path, List<Order> orders)
    {
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters = { new DateTimeWithoutTimeZoneConverter() }
        };
        
        var json = JsonSerializer.Serialize(orders, jsonSerializerOptions);
        
        try
        {
            File.WriteAllText(path, json);
            Log.Information($"Write orders on path {path}");
        }
        catch (Exception e)
        {
            Log.Error($"Error while writing a file: {path}. Error: {e.Message}");
        }
    }

    public List<Order>? ReadOrders(string path)
    {
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
        {
            Log.Error($"Not Found data file on directory {directory}");
            return default;
        }

        string json;
        try
        {
            json = File.ReadAllText(path);
        }
        catch (Exception e)
        {
            Log.Error($"Error while reading a file: {path}. Error: {e.Message}");
            return default;
        }
        
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters = { new DateTimeWithoutTimeZoneConverter() }
        };
        
        Log.Information($"Read orders on path {path}");
        return JsonSerializer.Deserialize<List<Order>>(json, jsonSerializerOptions);
    }
}