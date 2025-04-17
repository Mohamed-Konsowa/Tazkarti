namespace Tazkarti.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual User_Ticket? User_Tickets { get; set; }
    }
}
