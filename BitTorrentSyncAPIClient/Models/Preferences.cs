namespace BitTorrentSyncAPIClient.Models
{
    public class Preferences
    {
        public string device_name { get; set; }
        public string disk_low_priority { get; set; }
        public int download_limit { get; set; }
        public string folder_rescan_interval { get; set; }
        public string lan_encrypt_data { get; set; }
        public string lan_use_tcp { get; set; }
        public int lang { get; set; }
        public int listening_port { get; set; }
        public string max_file_size_diff_for_patching { get; set; }
        public string max_file_size_for_versioning { get; set; }
        public string rate_limit_local_peers { get; set; }
        public string send_buf_size { get; set; }
        public string sync_max_time_diff { get; set; }
        public string sync_trash_ttl { get; set; }
        public int upload_limit { get; set; }
        public int use_upnp { get; set; }
        public string recv_buf_size { get; set; }
    }
}
