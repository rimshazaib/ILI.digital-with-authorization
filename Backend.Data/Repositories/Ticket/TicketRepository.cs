using Backend.Application.DataTransferObjects.Ticket;
using Backend.Data.Entities.Ticket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Data.Repositories
{
    public class TicketRepository
    {
        private readonly EFDataContext _context;

        public TicketRepository(EFDataContext _context)
        {
            this._context = _context;
        }

        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Ticket.Include(i=> i.Movies).ToListAsync();
        }

        public async Task<Ticket> GetTicket(int id)
        {
            return await _context.Ticket.Include(i => i.Movies).Where(e => e.Tid == id).FirstOrDefaultAsync();
        }

        public async Task<Ticket> AddTicket(TicketDto ticket)
        {
            var movie = await _context.Movie.FirstOrDefaultAsync(e => e.Id == ticket.MovieId);
            //Ticket obj = new Ticket();
            //obj.Price = ticket.Price;
            //obj.MovieId = ticket.MovieId;
            //var result = await _context.Ticket.AddAsync(obj);

            var res = _context.Ticket.Add(
                new Ticket
                {
                    Price = ticket.Price,
                    MovieId = movie.Id,
                }

                );

            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Ticket> UpdateTicket(int id, TicketDto Ticket)
        {


            var result = await _context.Ticket
                .FirstOrDefaultAsync(e => e.Tid == id);

            if (result != null)
            {


                result.Price = Ticket.Price;
                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<dynamic> DeleteTicket(int id)
        {
            var result = await _context.Ticket
                .FirstOrDefaultAsync(e => e.Tid == id);
            if (result != null)
            {
                _context.Ticket.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
