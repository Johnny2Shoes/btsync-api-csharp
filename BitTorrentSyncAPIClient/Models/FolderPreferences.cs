namespace BitTorrentSyncAPIClient.Models
{
    public class FolderPreferences
    {
        public int search_lan { get; set; }
        public int use_dht { get; set; }
        public int use_hosts { get; set; }
        public int use_relay_server { get; set; }
        public int use_sync_trash { get; set; }
        public int use_tracker { get; set; }
    }
}
