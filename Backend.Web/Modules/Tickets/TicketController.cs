using Backend.Application.DataTransferObjects.Ticket;
using Backend.Data;
using Backend.Data.Entities.Ticket;
using Backend.Web.Modules.Tickets;
using HRM.Web.Controllers.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Web.Modules
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
 
        public class TicketController : BaseController
        {
            private readonly EFDataContext _context;
            private readonly TicketService ticketservice;
            public TicketController(EFDataContext context)
            {
                _context = context;
                ticketservice = new TicketService(context);
            }
            // GET: api/Movies
            [HttpGet]
            public async Task<dynamic> GetTickets()
            {
                return await ticketservice.GetTickets();
            }
            // GET: api/Movies/5
            [HttpGet("{id}")]
            public async Task<dynamic> GetTicket(int id)
            {
                return await ticketservice.GetTicket(id);
            }
            // PUT: api/Movies/5
            [HttpPut("{id}")]
            public async Task<dynamic> PutTicket(int id, TicketDto ticket)
            {
                return await ticketservice.PutTicket(id, ticket);
                
            }
            // POST: api/Movies
            [HttpPost]
            public IActionResult PostTicket(TicketDto ticket)
            {
               return Ok(ticketservice.PostTicket(ticket));
            }
            // DELETE: api/Movies/5
            [HttpDelete("{id}")]
            public async Task<dynamic> DeleteTicket(int id)
            {
                return await ticketservice.DeleteTicket(id);
               
            }
            private bool TicketExists(int id)
            {
                return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
            }
        }
}
