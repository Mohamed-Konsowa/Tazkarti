namespace Tazkarti.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string First_team { get; set; }
        public string Second_team { get; set; }
        public string Stade { get; set; }
        public string City { get; set; }
        public DateTime Time { get; set; }
        public int Price { get; set; }
        public int NumOfTickets { get; set; }
        public virtual User_Ticket? User_Tickets { get; set; }
    }
}
