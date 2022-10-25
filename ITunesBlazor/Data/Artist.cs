namespace ITunesBlazor.Data
{
    public class Artist
    {
        public long Id { get; set; }
        public ICollection<Album> Albums { get; set; }
        public string Name { get; set; }
    }
}