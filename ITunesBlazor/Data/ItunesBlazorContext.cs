using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITunesBlazor.Data
{
    public class ItunesBlazorContext : DbContext
    {
        public ItunesBlazorContext(DbContextOptions<ItunesBlazorContext> options) : base(options)
        {

        }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}
