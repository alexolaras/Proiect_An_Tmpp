using Proiect_An.Models.DesignPatterns.Decorator;
using Proiect_An.Models;

public class BasicRoomService : IRoomService
{
    private readonly Room _room;
    private readonly int _nights;
    public BasicRoomService(Room room, int nights)
    {
        _room = room;
        _nights = nights;
    }
    public string GetDescription() => _room?.Type ?? "";
    public double GetCost() => _room.PricePerNight * _nights;
}
