using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RippleRest
{
    public sealed class RippleRestClient
    {
        private static RippleRestClient defaultInstance = null;
        public static RippleRestClient DefaultInstance {
            get
            {
                return defaultInstance;
            }

            set
            {
                defaultInstance = value;
            }
        }

        internal static RippleRestClient GetDefaultInstanceOrThrow()
        {
            if (DefaultInstance == null)
                throw new ArgumentException("Default instance of RippleRestClient is not exist. ", "RippleRestClient");

            return DefaultInstance;
        }

        public RestSharp.RestClient RestClient
        {
            get;
            private set;
        }

        private JsonSerializer JsonSerializer;

        public RippleRestClient(string endpointURL)
        {
            RestClient = new RestClient(endpointURL);
            JsonSerializer = new JsonSerializer();
        }

        public String EndpointURL
        {
            get
            {
                return RestClient.BaseUrl;
            }

            set
            {
                RestClient.BaseUrl = value;
            }
        }

        internal RestRequest CreateGetRequest(string resource)
        {
            var request = new RestRequest(resource, Method.GET);
            request.JsonSerializer = JsonSerializer;

            return request;
        }

        internal RestRequest CreateGetRequest(string format, params object[] args)
        {
            return CreateGetRequest(String.Format(format, args));
        }

        internal void HandleResponseErrors(IRestResponse response)
        {
            if (response.ResponseStatus == ResponseStatus.Error)
            {
                if (response.ErrorException != null)
                    throw response.ErrorException;

                if (response.ErrorMessage != null)
                    throw new RippleRestException(response.ErrorMessage);

                throw new RippleRestException(response.StatusDescription);
            }
        }

        internal void HandleResponseErrors<T>(IRestResponse<T> response)
        {
            if (response.ResponseStatus == ResponseStatus.Error)
            {
                if (response.ErrorException != null)
                    throw response.ErrorException;

                if (response.ErrorMessage != null)
                    throw new RippleRestException(response.ErrorMessage);

                throw new RippleRestException(response.StatusDescription);
            }
        }

        internal void HandleRestResponseErrors<T>(IRestResponse<T> response)
            where T : RestResultObject
        {
            HandleResponseErrors(response);
            {
                var data = response.Data;
                if (!data.Success)
                    throw new RippleRestException(data);
            }
        }

        private class IsServerConnectedResult : RestResultObject
        {
            [JsonProperty("connected")]
            public bool Connected { set; get; }
        }

        public bool IsServerConnected()
        {
            var result = RestClient.Execute<IsServerConnectedResult>(CreateGetRequest("v1/server/connected"));
            HandleRestResponseErrors(result);
            return result.Data.Connected;
        }

        public ServerInfo GetServerInfo()
        {
            var result = RestClient.Execute<ServerInfo>(CreateGetRequest("v1/server"));
            HandleRestResponseErrors(result);
            return result.Data;
        }

        public object GetTransaction(string hash)
        {
            var result = RestClient.Execute(CreateGetRequest("v1/transactions/" + hash));
            HandleResponseErrors(result);
            return (Newtonsoft.Json.Linq.JContainer)JsonConvert.DeserializeObject(result.Content);
        }

        private class GenerateUUIDResult : RestResultObject
        {
            [JsonProperty("uuid")]
            public string UUID { set; get; }
        }

        public string GenerateUUID()
        {
            var result = RestClient.Execute<GenerateUUIDResult>(CreateGetRequest("v1/uuid"));
            HandleRestResponseErrors(result);
            return result.Data.UUID;
        }
    }
}
