namespace Proiect_An.Models.DesignPatterns.FactoryMethod
{
    public class ConcreteRoomFactory : IRoomFactory
    {
        public Room CreateRoom(string type, double price, string bedType, bool isAvailable)
        {
            return new Room { Type = type, PricePerNight = price, BedType = bedType, IsAvailable = isAvailable };
        }
    }
}
