using Microsoft.EntityFrameworkCore;
using MVC_Movie.Models;

namespace Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
        public DbSet<Anime> Animes { get; set; } = default!;
    }
}
