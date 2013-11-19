namespace BitTorrentSyncAPIClient.Models
{
    public class Folder
    {
        public string dir { get; set; }
        public string secret { get; set; }
        public long size { get; set; }
        public string type { get; set; }
        public long files { get; set; }
        public int error { get; set; }
        public int indexing { get; set; }
    }
}
