namespace BitTorrentSyncAPIClient.Models
{
    public class FolderPeer
    {
        public string id { get; set; }
        public string connection { get; set; }
        public string name { get; set; }
        public string synced { get; set; }
        public int download { get; set; }
        public long upload { get; set; }
    }
}
