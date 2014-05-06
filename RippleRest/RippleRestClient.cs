using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    /// <summary>
    /// Ripple Rest Client for .NET.
    /// </summary>
    public sealed class RippleRestClient
    {
        private static RippleRestClient defaultInstance = null;

        /// <summary>
        /// Set the default instance of RippleRestClient used by Account and other objects.
        /// </summary>
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

        /// <summary>
        /// Set the RestClient instance used for request.
        /// </summary>
        public RestSharp.RestClient RestClient
        {
            get;
            private set;
        }

        private JsonSerializer JsonSerializer;

        /// <summary>
        /// Create a new instance of RippleRestClient.
        /// </summary>
        /// <param name="endpointURL">Endpoint URI, like "http://localhost:5990/"</param>
        public RippleRestClient(string endpointURL)
        {
            RestClient = new RestClient(endpointURL);
            JsonSerializer = new JsonSerializer();
        }

        /// <summary>
        /// Endpoint URI, like "http://localhost:5990/"
        /// </summary>
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
            request.RequestFormat = DataFormat.Json;

            return request;
        }

        internal RestRequest CreateGetRequest(string format, params object[] args)
        {
            return CreateGetRequest(String.Format(format, args));
        }

        internal RestRequest CreatePostRequest(object data, string resource)
        {
            var request = new RestRequest(resource, Method.POST);
            request.JsonSerializer = JsonSerializer;
            request.RequestFormat = DataFormat.Json;
            request.AddBody(data);

            return request;
        }

        internal RestRequest CreatePostRequest(object data, string format, params object[] args)
        {
            return CreatePostRequest(data, String.Format(format, args));
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
            where T : RestResponseObject
        {
            HandleResponseErrors(response);
            {
                var data = response.Data;
                if (!data.Success)
                    throw new RippleRestException(data);
            }
        }

        [Serializable]
        [TypeConverter(typeof(SerializableExpandableObjectConverter))]
        private class IsServerConnectedResult : RestResponseObject
        {
            [JsonProperty("connected")]
            public bool Connected { set; get; }
        }

        /// <summary>
        /// A simple endpoint that can be used to check if ripple-rest is connected to a rippled and is ready to serve. If used before querying the other endpoints this can be used to centralize the logic to handle if rippled is disconnected from the Ripple Network and unable to process transactions.
        /// </summary>
        /// <returns>true if `ripple-rest` is ready to serve</returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
        public bool IsServerConnected()
        {
            var result = RestClient.Execute<IsServerConnectedResult>(CreateGetRequest("v1/server/connected"));
            HandleRestResponseErrors(result);
            return result.Data.Connected;
        }

        /// <summary>
        /// Retrieve information about the ripple-rest and connected rippled's current status.
        /// </summary>
        /// <returns>ServerInfo object. See https://github.com/ripple/ripple-rest/blob/develop/docs/api-reference.md#get-server-info fore more information</returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
        public ServerInfo GetServerInfo()
        {
            var result = RestClient.Execute<ServerInfo>(CreateGetRequest("v1/server"));
            HandleRestResponseErrors(result);
            return result.Data;
        }

        /// <summary>
        /// Retrieve the details of a transaction in the standard Ripple JSON format. 
        /// </summary>
        /// <param name="hash">Transaction hash</param>
        /// <returns></returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
        public object GetTransaction(string hash)
        {
            var result = RestClient.Execute(CreateGetRequest("v1/transactions/" + hash));
            HandleResponseErrors(result);
            return (Newtonsoft.Json.Linq.JContainer)JsonConvert.DeserializeObject(result.Content);
        }

        [Serializable]
        [TypeConverter(typeof(SerializableExpandableObjectConverter))]
        private class GenerateUUIDResult : RestResponseObject
        {
            [JsonProperty("uuid")]
            public string UUID { set; get; }
        }

        /// <summary>
        /// A UUID v4 generator.
        /// </summary>
        /// <returns>String "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx"</returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
        public string GenerateUUID()
        {
            var result = RestClient.Execute<GenerateUUIDResult>(CreateGetRequest("v1/uuid"));
            HandleRestResponseErrors(result);
            return result.Data.UUID;
        }
    }
}
