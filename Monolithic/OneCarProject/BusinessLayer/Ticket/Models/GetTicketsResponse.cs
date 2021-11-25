using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneCarProject.BusinessLayer.Ticket.Models
{
    public class GetTicketsResponse
    {
        [JsonProperty("ticketList")]
        public IList<TicketDTO> TicketList { get; set; }
    }
}
