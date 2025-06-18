namespace Proiect_An.Models.DesignPatterns.FactoryMethod
{
    public interface IRoomFactory
    {
        Room CreateRoom(string type, double price, string bedType, bool isAvailable);
    }
}
