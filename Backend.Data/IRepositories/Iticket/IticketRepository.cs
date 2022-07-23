using Backend.Application.DataTransferObjects.Ticket;
using Backend.Data.Entities.Ticket;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Data.IRepositories
{
    interface IticketRepository
    {
        Task<ActionResult<IEnumerable<Ticket>>> GetTickets();
        Task<Ticket> GetTicket(int id);
        Task<Ticket> AddTicket(TicketDto movie);
        Task<Ticket> UpdateTicket(int id, TicketDto movie);
        Task<dynamic> DeleteTicket(int id);
    }
}
