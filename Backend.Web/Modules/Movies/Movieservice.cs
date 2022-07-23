using Backend.Application.DataTransferObjects.Movie;
using Backend.Application.Enums;
using Backend.Application.Wrappers;
using Backend.Data;
using MovieAPI.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Web.Modules.Movies
{
    public class MovieService
    {
        private readonly EFDataContext _context;
        private readonly Imovierepository movieRepository;
       // private readonly movierepository movieRepository;
        public MovieService(EFDataContext appDbContext)
        {
            this._context = appDbContext;
            movieRepository = new movierepository(appDbContext);
        }
        // GET: api/Movies
        public async  Task<Response<dynamic>>GetMovies()
        {
            if (_context.Movie == null)
            {
                return new Response<dynamic>(true, _context.Movie, GeneralMessage.FileNotFound);
            }
            var res= await movieRepository.GetMovies(); ;
            return new Response<dynamic>(true, res, GeneralMessage.GetRecord);
        }
            // GET: api/Movies/5
            public async Task<Response<dynamic>> GetMovie(int id)
        {
            if (_context.Movie == null)
            {
                return new Response<dynamic>(true, _context.Movie, GeneralMessage.FileNotFound);
            }
            var movie = await movieRepository.GetMovie(id);
            if (movie == null)
            {
                return new Response<dynamic>(true, movie, GeneralMessage.FileNotFound);
            }
            return new Response<dynamic>(true, movie, GeneralMessage.GetRecord);
        }
        // PUT: api/Movies/5
        public async Task<dynamic> PutMovie(int id, MovieDto movie)
        {
            var result = await movieRepository.UpdateMovie(id, movie);
            if (result == null)
            {
                return new Response<dynamic>(true, result, GeneralMessage.INVALIDCREDENTIALS);
            }
            else
                return new Response<dynamic>(true, result, GeneralMessage.RecordUpdated);
        }
        // POST: api/Movies
        public Response<dynamic> PostMovie(MovieDto movie)
        {
            if (movie == null)
            {
                return new Response<dynamic>(true, movie, GeneralMessage.INVALIDCREDENTIALS);
            }
            else
            {
                var result = movieRepository.AddMovie(movie);
                return new Response<dynamic>(true, result.Result, GeneralMessage.RecordAdded);
            }
        }
            // DELETE: api/Movies/5
            public async Task<dynamic> DeleteMovie(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return new Response<dynamic>(true, movie, GeneralMessage.INVALIDCREDENTIALS);
            }
            var result = await movieRepository.DeleteMovie(id);
            return new Response<dynamic>(true, result, GeneralMessage.RecordDeleted);
        }
        private bool MovieExists(int id)
        {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
