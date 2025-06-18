namespace Proiect_An.Models.DesignPatterns.Decorator
{
    public class LateCheckoutDecorator : IRoomService
    {
        private readonly IRoomService _inner;
        public LateCheckoutDecorator(IRoomService inner) { _inner = inner; }
        public string GetDescription() => _inner.GetDescription() + " + Late Checkout";
        public double GetCost() => _inner.GetCost() + 30;
    }

}
