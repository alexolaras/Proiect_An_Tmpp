namespace Proiect_An.Models.DesignPatterns.FactoryMethod
{
    public class LuxuryRoomFactory: IRoomFactory
    {
        public Room CreateRoom(string type, double price, string bedType, bool isAvailable)
        {
            return new Room
            {
                Type = "Deluxe",
                PricePerNight = 180,
                BedType = "King",
                IsAvailable = true
            };
        }
    }
}
