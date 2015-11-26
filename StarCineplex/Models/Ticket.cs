namespace StarCineplex.Models
{
    public class Ticket
    {
        public string Slot { get; set; }
        public string Showtype { get; set; }
        public string Venue { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public string Days { get; set; }
        public string Comment { get; set; }
    }
}