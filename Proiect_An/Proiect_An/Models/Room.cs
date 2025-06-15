namespace Proiect_An.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Type { get; set; } = "";
        public double PricePerNight { get; set; }
        public string BedType { get; set; } = "";
        public bool IsAvailable { get; set; } = true;
    }
    
}
