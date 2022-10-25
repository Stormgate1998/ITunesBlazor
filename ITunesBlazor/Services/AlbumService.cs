using ITunesBlazor.Data;
using ITunesBlazor.Pages;
using Microsoft.EntityFrameworkCore;

namespace ITunes.Services
{
    public class AlbumService
    {
        private readonly ItunesBlazorContext context;

        public AlbumService(ItunesBlazorContext context)
        {
            this.context = context;
        }

        internal async Task CreateAlbumAsync(string title, string artistName)
        {
            var artist = context.Artists.Where(a => a.Name == artistName).FirstOrDefault();
            if (artist is null)
            { //Just create a new artist if it doesn't exsist
                context.Add<Artist>(new Artist { Name = artistName });
                await context.SaveChangesAsync();
                artist = context.Artists.Where(a => a.Name == artistName).FirstOrDefault();
            }
            context.Add<Album>(new Album { Artist = artist, Title = title });
            await context.SaveChangesAsync();
        }

        internal async Task DeleteAlbum(long albumId)
        {
            var album = context.Albums.Where(a => a.Id == albumId).Include(a => a.Tracks).FirstOrDefault();
            if (album != null)
            {
                context.Albums.Remove(album);
                await context.SaveChangesAsync();
            }
        }

        internal async Task EditAlbumAsync(long albumId, string title, string artistName)
        {
            var album = context.Albums.Where(a => a.Id == albumId).Include(a => a.Artist).Include(a => a.Tracks).FirstOrDefault();
            if (album != null)
            {
                album.Title = title;
                album.Artist.Name = artistName;
                context.Albums.Update(album);
                await context.SaveChangesAsync();
            }
        }

        internal async Task<Album?> GetAlbumAsync(long albumId)
        {
            return await context.Albums.Where(a => a.Id == albumId).Include(a => a.Artist).Include(a => a.Tracks).FirstOrDefaultAsync();
        }

        internal async Task<IEnumerable<Album>> GetAlbumsAsync()
        {
            return await context.Albums.Include(a => a.Artist).ToListAsync();
        }
    }
}
