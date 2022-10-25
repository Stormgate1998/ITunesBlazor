using ITunesBlazor.Data;
using Microsoft.EntityFrameworkCore;

namespace ITunes.Services
{
    public class ArtistService
    {
        private readonly ItunesBlazorContext context;

        public ArtistService(ItunesBlazorContext context)
        {
            this.context = context;
        }

        internal async Task DeleteArtist(long artistId)
        {
            var artist = context.Artists.Where(a => a.Id == artistId).FirstOrDefault();
            if (artist != null)
            {
                context.Artists.Remove(artist);
                await context.SaveChangesAsync();
            }
        }

        internal async Task<IEnumerable<Artist>> GetArtistsAsync()
        {
            return await context.Artists.ToListAsync();
        }

        internal async Task EditArtistAsync(long artistId, string newArtistName)
        {
            var artist = await context.Artists.Where(a => a.Id == artistId).FirstOrDefaultAsync();
            if (artist != null)
            {
                artist.Name = newArtistName;
                context.Artists.Update(artist);
                await context.SaveChangesAsync();
            }
        }

        internal async Task<Artist> GetArtistAsync(int artistid)
        {
            return await context.Artists.Where(a => a.Id == artistid).Include(a => a.Albums).ThenInclude(a => a.Tracks).FirstOrDefaultAsync();
        }
    }
}
