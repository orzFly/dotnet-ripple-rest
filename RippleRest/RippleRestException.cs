using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RippleRest
{
    [Serializable]
    public class RippleRestException : Exception
    {
        public RestResultObject RestObject;

        public RippleRestException() { }
        public RippleRestException(string message) : base(message) { }
        public RippleRestException(string message, Exception inner) : base(message, inner) { }
        public RippleRestException(RestResultObject restObject) : this(restObject, null) { }
        public RippleRestException(RestResultObject restObject, Exception inner)
            : base(restObject.Message ?? restObject.Error, inner)
        {
            this.RestObject = restObject;
        }
        protected RippleRestException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
