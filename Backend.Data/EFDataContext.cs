using Backend.Data.Entities.Test;
using Backend.Data.Entities.Ticket;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using Backend.Data.Entities.registration;


namespace Backend.Data
{
    public partial class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions options) : base(options)
        {
        
        }
        public DbSet<Test> Test { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Register> Register { get; set; }
    }
}
