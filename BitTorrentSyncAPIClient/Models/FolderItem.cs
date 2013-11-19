namespace BitTorrentSyncAPIClient.Models
{
    public class FolderItem
    {
        public string name { get; set; }
        public string state { get; set; }
        public string type { get; set; }
        public int have_pieces { get; set; }
        public long size { get; set; }
        public int total_pieces { get; set; }
        public int download { get; set; }
    }
}
