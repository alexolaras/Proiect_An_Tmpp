namespace Proiect_An.Models.DesignPatterns.Decorator
{
    public class SpaDecorator : IRoomService
    {
        private readonly IRoomService _inner;
        public SpaDecorator(IRoomService inner) { _inner = inner; }
        public string GetDescription() => _inner.GetDescription() + " + Spa Access";
        public double GetCost() => _inner.GetCost() + 50;
    }

}
