using Backend.Application.DataTransferObjects.Ticket;
using Backend.Application.Enums;
using Backend.Application.Wrappers;
using Backend.Data;
using Backend.Data.Entities.Ticket;
using Backend.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Web.Modules.Tickets
{
    public class TicketService
    {
        private readonly EFDataContext _context;
        private readonly TicketRepository ticketRepository;
        public TicketService(EFDataContext appDbContext)
        {
            this._context = appDbContext;
            ticketRepository = new TicketRepository(appDbContext);
        }
        // GET: api/Movies

        public async Task<Response<dynamic>> GetTickets()
        {
            if (_context.Ticket == null)
            {
                return new Application.Wrappers.Response<dynamic>(true, _context.Ticket, GeneralMessage.INVALIDCREDENTIALS);
            }
            var res =  await ticketRepository.GetTickets();
            return new Response<dynamic>(true, res, GeneralMessage.GetRecord);
        }
        // GET: api/Movies/5
        public async Task<Response<dynamic>> GetTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return new Application.Wrappers.Response<dynamic>(true, _context.Ticket, GeneralMessage.INVALIDCREDENTIALS);
            }
            var ticket = await ticketRepository.GetTicket(id);
            if (ticket == null)
            {
                return new Response<dynamic>(true, ticket, GeneralMessage.INVALIDCREDENTIALS);
            }
            return new Response<dynamic>(true, ticket, GeneralMessage.GetRecord);
        }
        // PUT: api/Movies/5
        public async Task<dynamic> PutTicket(int id, TicketDto ticket)
        {
            var result = await ticketRepository.UpdateTicket(id, ticket);
            if (result == null)
            {
                return new Response<dynamic>(true, result, GeneralMessage.INVALIDCREDENTIALS);
            }
            else
                return new Response<dynamic>(true, result, GeneralMessage.ChangesSaved);
        }
        // POST: api/Movies
        public  Response<dynamic> PostTicket(TicketDto ticket)
        {
            if (ticket == null)
            {
                return new Response<dynamic>(true, ticket, GeneralMessage.INVALIDCREDENTIALS);
            }
            else
            {
                var result = ticketRepository.AddTicket(ticket);
                return new Response<dynamic>(true, result.Result, GeneralMessage.RecordAdded);
            }
        }
        // DELETE: api/Movies/5
        public async Task<dynamic> DeleteTicket(int id)
        {
            var movie = await _context.Ticket.FindAsync(id);
            if (movie == null)
            {
                return new Response<dynamic>(true, movie, GeneralMessage.INVALIDCREDENTIALS);
            }
            var result = await ticketRepository.DeleteTicket(id);
            return new Response<dynamic>(true, result, GeneralMessage.RecordDeleted);
        }
        private bool MovieExists(int id)
        {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
