using Proiect_An.Models.DesignPatterns.Decorator;
using Proiect_An.Models;
using static Proiect_An.Models.Enums.RoomService;

public static class BookingDecoratorHelper
{
    private static readonly Dictionary<RoomServiceType, Func<IRoomService, IRoomService>> decoratorMap =
        new()
        {
            { RoomServiceType.Breakfast, s => new BreakfastDecorator(s) },
            { RoomServiceType.Spa, s => new SpaDecorator(s) },
            { RoomServiceType.LateCheckout, s => new LateCheckoutDecorator(s) }
        };

    public static double CalculateTotal(Room room, DateTime checkIn, DateTime checkOut, IEnumerable<string> selectedServiceStrings)
    {
        var selectedServices = selectedServiceStrings
            .Select(s => Enum.Parse<RoomServiceType>(s))
            .ToList();

        int nights = (checkOut - checkIn).Days;
        if (nights < 1) nights = 1;

        IRoomService service = new BasicRoomService(room, nights);
        foreach (var s in selectedServices)
        {
            if (decoratorMap.ContainsKey(s))
                service = decoratorMap[s](service);
        }
        return service.GetCost();
    }
    public static string GetDescription(Room room, DateTime checkIn, DateTime checkOut, IEnumerable<string> selectedServiceStrings)
    {
        var selectedServices = selectedServiceStrings
    .Where(s => !string.IsNullOrWhiteSpace(s))
    .Select(s => Enum.Parse<RoomServiceType>(s))
    .ToList();



        int nights = (checkOut - checkIn).Days;
        if (nights < 1) nights = 1;

        IRoomService service = new BasicRoomService(room, nights);
        foreach (var s in selectedServices)
        {
            if (decoratorMap.ContainsKey(s))
                service = decoratorMap[s](service);
        }
        return service.GetDescription();
    }
}
