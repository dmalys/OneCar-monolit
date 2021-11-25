using Newtonsoft.Json;

namespace OneCarProject.BusinessLayer.Ticket.Models
{
    public class GetTicketResponse
    {
        [JsonProperty("ticketList")]
        public TicketDTO Ticket { get; set; }
    }
}
