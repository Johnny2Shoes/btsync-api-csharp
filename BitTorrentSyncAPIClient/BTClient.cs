using System;
using System.Collections.Generic;
using RestSharp;
using System.Net;
using BitTorrentSyncAPIClient.Models;

namespace BitTorrentSyncAPIClient
{
    public class BTClient
    {
        private readonly RestClient client;

        public BTClient(string baseURL, string username, string password)
        {
            client = new RestClient(baseURL)
            {
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
        }

        public T Execute<T>(RestRequest request, HttpStatusCode expectedResponseCode) where T : new()
        {
            // Won't throw exception.
            var response = client.Execute<T>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || response.ErrorException != null)
                throw new Exception(
                      "RestSharp response status: " + response.ResponseStatus + " - HTTP response: " + response.StatusCode + " - " + response.StatusDescription
                    + " - " + response.Content);
            else
                return response.Data;
        }

        public Response AddFolder(string directory, string secret)
        {
            var request = new RestRequest();
            request.Resource = "/api?method=add_folder&dir=" + directory + "&secret=" + secret;
            request.Method = Method.GET;

            return Execute<Response>(request, HttpStatusCode.OK);
        }

        public List<Folder> GetFolders(string secret)
        {
            var request = new RestRequest();
            request.Resource = "/api?method=get_folders";
            if (secret.Length > 0)
            {
                request.Resource += "&secret=" + secret;
            }
            request.Method = Method.GET;

            return Execute<List<Folder>>(request, HttpStatusCode.OK);
        }

        public Response RemoveFolder(string secret)
        {
            var request = new RestRequest();
            request.Resource = "/api?method=remove_folder&secret=" + secret;
            request.Method = Method.GET;

            return Execute<Response>(request, HttpStatusCode.OK);
        }

        public Secrets GetSecrets()
        {
            var request = new RestRequest();
            request.Resource = "/api?method=get_secrets";
            request.Method = Method.GET;

            return Execute<Secrets>(request, HttpStatusCode.OK);
        }

        public List<FolderItem> GetFiles(string secret, string path)
        {
            var request = new RestRequest();
            request.Resource = "/api?method=get_files&secret=" + secret;
            if (path.Length > 0)
            {
                request.Resource += "&path=" + path;
            }
            request.Method = Method.GET;

            return Execute<List<FolderItem>>(request, HttpStatusCode.OK);
        }

        public Preferences GetPreferences()
        {
            var request = new RestRequest();
            request.Resource = "/api?method=get_prefs";
            request.Method = Method.GET;

            return Execute<Preferences>(request, HttpStatusCode.OK);
        }

        public void SetPreferences()
        {
            throw new Exception("Not Implemented");
        }

        public OSNameResponse GetOSName()
        {
            var request = new RestRequest();
            request.Resource = "/api?method=get_os";
            request.Method = Method.GET;

            return Execute<OSNameResponse>(request, HttpStatusCode.OK);
        }

        public ClientVersion GetVersion()
        {
            var request = new RestRequest();
            request.Resource = "/api?method=get_version";
            request.Method = Method.GET;

            return Execute<ClientVersion>(request, HttpStatusCode.OK);
        }

        public SpeedResponse GetSpeed()
        {
            var request = new RestRequest();
            request.Resource = "/api?method=get_speed";
            request.Method = Method.GET;

            return Execute<SpeedResponse>(request, HttpStatusCode.OK);
        }

        public Response Shutdown()
        {
             var request = new RestRequest();
            request.Resource = "/api?method=shutdown";
            request.Method = Method.GET;

            return Execute<Response>(request, HttpStatusCode.OK);
        }

        public Response SetFilePreferences(string secret, string path, bool download)
        {
            var request = new RestRequest();
            request.Resource = "/api?method=set_file_prefs&secret=" + secret + "&path=" + path + "&download=";
            if (download)
            {
                request.Resource = "1";
            }
            else
            {
                request.Resource = "0";
            }
            request.Method = Method.GET;

            return Execute<Response>(request, HttpStatusCode.OK);
        }

        public List<FolderPeer> GetFolderPeers(string secret)
        {
            var request = new RestRequest();
            request.Resource = "/api?method=get_folder_peers&secret=" + secret;
            request.Method = Method.GET;

            return Execute<List<FolderPeer>>(request, HttpStatusCode.OK);
        }

        public FolderPreferences GetFolderPreferences(string secret)
        {
            var request = new RestRequest();
            request.Resource = "/api?method=get_folder_prefs&secret=" + secret;
            request.Method = Method.GET;

            return Execute<FolderPreferences>(request, HttpStatusCode.OK);
        }

        public Response SetFolderPreferences(string secret, FolderPreferences folderPreferences)
        {
            var request = new RestRequest();
            request.Resource = "/api?method=set_folder_prefs&secret=" + secret;
            request.AddParameter(new Parameter()
            {
                Name = "search_lan",
                Value = folderPreferences.search_lan,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "use_dht",
                Value = folderPreferences.use_dht,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "use_hosts",
                Value = folderPreferences.use_hosts,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "use_relay_server",
                Value = folderPreferences.use_relay_server,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "use_sync_trash",
                Value = folderPreferences.use_sync_trash,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "use_tracker",
                Value = folderPreferences.use_tracker,
                Type = ParameterType.GetOrPost
            });
            request.Method = Method.GET;

            return Execute<Response>(request, HttpStatusCode.OK);
        }

        public FolderHosts GetFolderHosts(string secret, FolderHosts hosts)
        {
            var request = new RestRequest();
            request.Resource = "/api?method=get_folder_hosts&secret=" + secret;
            request.Method = Method.GET;

            return Execute<FolderHosts>(request, HttpStatusCode.OK);
        }

        public Response SetFolderHosts(string secret, FolderHosts hosts)
        {
            var request = new RestRequest();
            request.Resource = "/api?method=set_folder_hosts&secret=" + secret + "&hosts=";
            bool first = true;
            foreach (string hostEntry in hosts.hosts)
            {
                if (first)
                {
                    request.Resource += ",";
                    first = false;
                }
                request.Resource += hostEntry;
            }
            request.Method = Method.GET;

            return Execute<Response>(request, HttpStatusCode.OK);
        }

        public Response SetPreferences(string secret, Preferences prefs)
        {
            var request = new RestRequest();
            request.Resource = "/api?method=set_prefs&secret=" + secret;
            request.AddParameter(new Parameter()
            {
                Name = "device_name",
                Value = prefs.device_name,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "disk_low_priority",
                Value = prefs.disk_low_priority,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "download_limit",
                Value = prefs.download_limit,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "folder_rescan_interval",
                Value = prefs.folder_rescan_interval,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "lan_encrypt_data",
                Value = prefs.lan_encrypt_data,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "lan_use_tcp",
                Value = prefs.lan_use_tcp,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "lang",
                Value = prefs.lang,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "listening_port",
                Value = prefs.listening_port,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "max_file_size_diff_for_patching",
                Value = prefs.max_file_size_diff_for_patching,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "max_file_size_for_versioning",
                Value = prefs.max_file_size_for_versioning,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "rate_limit_local_peers",
                Value = prefs.rate_limit_local_peers,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "recv_buf_size",
                Value = prefs.recv_buf_size,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "send_buf_size",
                Value = prefs.send_buf_size,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "sync_max_time_diff",
                Value = prefs.sync_max_time_diff,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "sync_trash_ttl",
                Value = prefs.sync_trash_ttl,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "upload_limit",
                Value = prefs.upload_limit,
                Type = ParameterType.GetOrPost
            });
            request.AddParameter(new Parameter()
            {
                Name = "use_upnp",
                Value = prefs.use_upnp,
                Type = ParameterType.GetOrPost
            });

            request.Method = Method.GET;

            return Execute<Response>(request, HttpStatusCode.OK);
        }
    }
}
