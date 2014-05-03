using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RippleRest
{
    /// <summary>
    /// Throws if an exception is thrown by the Endpoint
    /// </summary>
    [Serializable]
    public class RippleRestException : Exception
    {
        /// <summary>
        /// The ResultResultObject associated.
        /// </summary>
        public RestResultObject RestResultObject;

        /// <summary>
        /// Create a new instance of this exception.
        /// </summary>
        public RippleRestException() { }

        /// <summary>
        /// Create a new instance of this exception.
        /// </summary>
        /// <param name="message">Error message.</param>
        public RippleRestException(string message) : base(message) { }

        /// <summary>
        /// Create a new instance of this exception.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="inner">Inner exception.</param>
        public RippleRestException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Create a new instance of this exception.
        /// </summary>
        /// <param name="restResultObject">The ResultResultObject associated.</param>
        public RippleRestException(RestResultObject restResultObject) : this(restResultObject, null) { }

        /// <summary>
        /// Create a new instance of this exception.
        /// </summary>
        /// <param name="restResultObject">The ResultResultObject associated.</param>
        /// <param name="inner">Inner exception</param>
        public RippleRestException(RestResultObject restResultObject, Exception inner)
            : base(restResultObject.Message ?? restResultObject.Error, inner)
        {
            this.RestResultObject = restResultObject;
        }

        /// <summary>
        /// Used for deserialization.
        /// </summary>
        /// <param name="info">Serialization Info</param>
        /// <param name="context">Streaming Context</param>
        protected RippleRestException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
