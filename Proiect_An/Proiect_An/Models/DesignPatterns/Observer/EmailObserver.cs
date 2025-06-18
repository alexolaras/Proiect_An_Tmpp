using Proiect_An.Models.DesignPatterns.Adapter;

namespace Proiect_An.Models.DesignPatterns.Observer
{
    public class EmailObserver : IBookingObserver
    {
        private readonly INotifier _notifier;

        public EmailObserver() =>
            _notifier = new EmailAdapter(new LegacyEmailSender());

        public void Update(Booking booking)
        {

            _notifier.Send(
                to: booking.Guest.Address,
                message: $"Booking #{booking.Id} confirmed for room {booking.RoomId}. The total price will be: {booking.TotalPrice}");
        }
    }
}
