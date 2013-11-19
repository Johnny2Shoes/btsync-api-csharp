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

    }
}
