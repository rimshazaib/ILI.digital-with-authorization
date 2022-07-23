using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Backend.Application.DataTransferObjects.Movie;

namespace MovieAPI.Repository
{
    public class movierepository : Imovierepository
    {

        private readonly EFDataContext _context;

        public movierepository(EFDataContext _context)
        {
            this._context = _context;
        }

        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movie.Include(i => i.Ticket).ToListAsync();
        }

        public async Task<Movie> GetMovie(int id)
        {
            return await _context.Movie.Include(i => i.Ticket)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Movie> AddMovie(MovieDto movie)
        {
            Movie obj = new Movie();
            obj.Title = movie.Title;
            obj.Genre = movie.Genre;
            obj.ReleaseDate = DateTime.Now;


            var result = await _context.Movie.AddAsync(obj);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Movie> UpdateMovie(int id, MovieDto movie)
        {
            var result = await _context.Movie.Include(i => i.Ticket)
                .FirstOrDefaultAsync(e => e.Id ==id);

            if (result != null)
            {

                if(movie.Title != null)
                    result.Title = movie.Title;
                if (movie.Genre != null)
                    result.Genre = movie.Genre;
                result.ReleaseDate = DateTime.Now;


                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async  Task<dynamic> DeleteMovie(int id)
        {
            var result = await _context.Movie
                .FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _context.Movie.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

 
    }
}
