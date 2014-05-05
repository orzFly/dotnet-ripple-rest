using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    /// <summary>
    /// Wrapper class for all RestObject used for Response.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public abstract class RestRequestObject
    {
        /// <summary>
        /// Account's secret.
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; set; }

        /// <summary>
        /// Client Resource ID.
        /// </summary>
        [JsonProperty("client_resource_id")]
        public string ClientResourceId { get; set; }
    }
}
