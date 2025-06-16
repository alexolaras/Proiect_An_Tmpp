namespace Proiect_An.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
