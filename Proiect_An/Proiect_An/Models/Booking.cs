namespace Proiect_An.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string GuestName { get; set; } = "";
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
