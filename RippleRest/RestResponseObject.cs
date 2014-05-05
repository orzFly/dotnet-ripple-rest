using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace RippleRest
{
    /// <summary>
    /// Wrapper class for all RestObject used for Response.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public abstract class RestResponseObject
    {
        /// <summary>
        /// A bool determines if the request if successful.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// The error message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// The error code.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
