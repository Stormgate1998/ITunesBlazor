namespace ITunesBlazor.Data
{
    public class Song
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public Album Album { get; set; }
        public Artist Artist { get; set; }
    }
}