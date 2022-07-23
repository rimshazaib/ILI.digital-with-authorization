using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.DataTransferObjects.Ticket
{
    public class TicketDto
    {
        [Required]
        public int Price { get; set; }
        [Required]
        public int MovieId { get; set; }
    }
}
