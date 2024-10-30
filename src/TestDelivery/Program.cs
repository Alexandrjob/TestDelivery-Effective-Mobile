using Serilog;
using TestDelivery.Models;
using TestDelivery.Services;

const string  PATH_DATA_ORDERS = "common/data/orders.json";

long _cityDistrict;
DateTime _firstDeliveryDateTime;
string _deliveryLog;
string _deliveryOrder;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("common/logs/logs.json", rollingInterval: RollingInterval.Day)
    .CreateLogger();

if (args.Length == 4)
{
    if (!long.TryParse(args[0], out _cityDistrict))
    {
        Log.Error($"Incorrect district id format: {args[0]}");
        return 0;
    }
    
    if (!DateTime.TryParse(args[1], out _firstDeliveryDateTime))
    {
        Log.Error("Incorrect time format.");
        return 0;
    }

    _deliveryLog = args[2];
    _deliveryOrder = args[3];
}
else
{
    _cityDistrict = GetValidatedCityDistrict();
    _firstDeliveryDateTime = GetValidatedFirstDeliveryDateTime();
    _deliveryLog = GetValidatedPathLogs();
    _deliveryOrder = GetValidatedOrderLogs();
}

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(_deliveryLog, rollingInterval: RollingInterval.Day)
    .CreateLogger();

var file = new FileService();

var orders = file.ReadOrders(PATH_DATA_ORDERS);

if (orders == null || orders.Count == 0)
{
    Log.Warning("File is empty");
    return 0;
}

var filter = new FilterService();
var result = filter.OrdersForDeliveryWithinNextMinutes(orders, new District(_cityDistrict, "empty"), _firstDeliveryDateTime,
    new DateTime(_firstDeliveryDateTime.Ticks).AddMinutes(30));

file.WriteOrders(_deliveryOrder, result);

long GetValidatedCityDistrict()
{
    while (true)
    {
        Console.Write("Enter the district number (integer): ");
        string input = Console.ReadLine();

        if (long.TryParse(input, out var result))
            return result;

        Console.WriteLine("Incorrect input. Please enter an integer.");
    }
}

DateTime GetValidatedFirstDeliveryDateTime()
{
    while (true)
    {
        Console.Write("Enter the date and time of delivery (format: YYYYY-MM-DD HH:MM:SS): ");
        var input = Console.ReadLine();

        if (DateTime.TryParse(input, out var result))
            return result;

        Console.WriteLine("Incorrect entry. Please enter the date and time in the correct format.");
    }
}

string GetValidatedPathLogs()
{
    while (true)
    {
        Console.Write("Enter the path to the logging file: ");
        var input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
            return input;

        Console.WriteLine("Incorrect input. The path cannot be empty.");
    }
}

string GetValidatedOrderLogs()
{
    while (true)
    {
        Console.Write("Enter the path to the result file: ");
        var input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
            return input;

        Console.WriteLine("Incorrect input. The path cannot be empty.");
    }
}

return 1;