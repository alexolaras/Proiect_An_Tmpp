namespace Proiect_An.Models.DesignPatterns.Observer
{
    public class BookingNotifier
    {
        private readonly List<IBookingObserver> _observers = new();

        public void Attach(IBookingObserver observer) => _observers.Add(observer);
        public void Detach(IBookingObserver observer) => _observers.Remove(observer);

        public void Notify(Booking booking)
        {
            foreach (var o in _observers) o.Update(booking);
        }
    }
}
