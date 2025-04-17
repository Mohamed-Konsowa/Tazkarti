using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tazkarti.Models
{
    public class User_Ticket
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey("Ticket")]

        public int Ticket_Id { get; set; }
        public virtual Ticket? Ticket { get; set; }

    }
}
