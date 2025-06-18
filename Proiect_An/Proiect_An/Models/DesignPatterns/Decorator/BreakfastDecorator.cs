namespace Proiect_An.Models.DesignPatterns.Decorator
{
    public class BreakfastDecorator : IRoomService
    {
        private readonly IRoomService _inner;
        public BreakfastDecorator(IRoomService inner) { _inner = inner; }
        public string GetDescription() => _inner.GetDescription() + " + Breakfast";
        public double GetCost() => _inner.GetCost() + 20;
    }

}
