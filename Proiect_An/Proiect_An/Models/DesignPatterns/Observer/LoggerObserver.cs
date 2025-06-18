using Proiect_An.Models.DesignPatterns.Singleton;

namespace Proiect_An.Models.DesignPatterns.Observer
{
    public class LoggerObserver : IBookingObserver
    {
        public void Update(Booking booking)
        {
            Logger.Instance.Log(
                $"[Observer] Booking #{booking.Id} – Guest {booking.GuestId}, Room {booking.RoomId}");
        }
    }
}
