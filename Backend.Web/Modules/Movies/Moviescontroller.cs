using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.DataTransferObjects.Movie;
using Backend.Data;
using Backend.Web.Modules.Movies;
using Microsoft.AspNetCore.Authorization;
using HRM.Web.Controllers.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : BaseController
    {
        private readonly EFDataContext _context;
        private readonly MovieService movieservice;
        public MoviesController(EFDataContext context)
        {
            _context = context;
            movieservice = new MovieService(context);
        }
        // GET: api/Movies
        [HttpGet]
        public async Task<dynamic> GetMovies()
        {
            return await movieservice.GetMovies();
        }
        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<dynamic> GetMovie(int id)
        {
             return await movieservice.GetMovie(id);
        }
        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<dynamic> PutMovie(int id, MovieDto movie)
        {
            return await movieservice.PutMovie(id, movie);
           
        }
        // POST: api/Movies
        [HttpPost]
        public IActionResult PostMovie(MovieDto movie)
        {
           return Ok( movieservice.PostMovie(movie));
           
        }
        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<dynamic> DeleteMovie(int id)
        {
            var result = await movieservice.DeleteMovie(id);
            return result;
        }
        private bool MovieExists(int id)
        {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
