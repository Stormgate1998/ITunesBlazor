using ITunesBlazor.Pages;
using ITunesBlazor.Data;
using Microsoft.EntityFrameworkCore;

namespace ITunes.Services
{
    public class SongService
    {
        private readonly ItunesBlazorContext context;

        public SongService(ItunesBlazorContext context)
        {
            this.context = context;
        }

        internal async Task CreateNewSongAsync(string songTitle, string artistName, string albumTitle)
        {
            var artist = context.Artists.Where(a => a.Name == artistName).FirstOrDefault();
            var album = context.Albums.Where(a => a.Title == albumTitle).FirstOrDefault();
            if (artist is null)
            { //Just create a new artist if it doesn't exsist
                context.Add<Artist>(new Artist { Name = artistName });
                await context.SaveChangesAsync();
                artist = context.Artists.Where(a => a.Name == artistName).FirstOrDefault();
            }
            if (album is null)
            { //Just create a new album if it doesn't exsist
                context.Add<Album>(new Album { Title = albumTitle, Artist = artist });
                await context.SaveChangesAsync();
                album = context.Albums.Where(a => a.Title == albumTitle).FirstOrDefault();
            }

            context.Add<Song>(new Song { Artist = artist, Title = songTitle, Album = album });
            await context.SaveChangesAsync();
        }

        internal async Task DeleteSongAsync(long songId)
        {
            var song = context.Songs.Where(a => a.Id == songId).Include(a => a.Artist).Include(a => a.Album).FirstOrDefault();
            if (song != null)
            {
                context.Songs.Remove(song);
                await context.SaveChangesAsync();
                
            }
        }

        internal async Task SubmitEditAsync(long songId, string songTitle, string artistName, string albumTitle)
        {

            var song = context.Songs.Where(a => a.Id == songId).Include(a => a.Artist).Include(a => a.Album).FirstOrDefault();
            if (song != null)
            {
                song.Title = songTitle;
                song.Artist.Name = artistName;
                song.Album.Title = albumTitle;
                context.Songs.Update(song);
                await context.SaveChangesAsync();
            }
        }

        internal async Task<Song> GetSong(long songId)
        {
            var song =context.Songs.Where(a => a.Id == songId).Include(a => a.Artist).Include(a => a.Album).FirstOrDefault();
            return song;
        }

        internal async Task<ICollection<Song>> GetSongList()
        {
           var songs = context.Songs.Include(a => a.Artist).Include(a => a.Album).ToList();
           return songs;
        }
    }
}
