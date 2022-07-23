using Backend.Application.DataTransferObjects.Movie;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Repository
{
    public interface Imovierepository
    {
        Task<ActionResult<IEnumerable<Movie>>> GetMovies();
        Task<Movie> GetMovie(int id);

        Task<Movie> AddMovie(MovieDto movie);
        Task<Movie> UpdateMovie(int id, MovieDto movie);
        Task<dynamic> DeleteMovie(int id);

    }
}



