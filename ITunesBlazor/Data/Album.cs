namespace ITunesBlazor.Data
{
    public class Album
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateOnly PublishedOn { get; set; }
        public Artist Artist { get; set; }
        public ICollection<Song> Tracks { get; set; }
    }
}