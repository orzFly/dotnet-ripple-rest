using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    /// <summary>
    /// A simplified representation of an account Balance
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public partial class Balance : RestObject
    {
        /// <summary>
        /// The quantity of the currency, denoted as a string to retain floating point precision
        /// </summary>
        [JsonProperty("value", Required = Required.Always)]
        public string Value { get; set; }

        /// <summary>
        /// The currency expressed as a three-character code
        /// </summary>
        /// <remarks><code language="RegExp">^([a-zA-Z0-9]{3}|[A-Fa-f0-9]{40})$</code></remarks>
        [RegexpPattern("^([a-zA-Z0-9]{3}|[A-Fa-f0-9]{40})$")]
        [JsonProperty("currency", Required = Required.Always)]
        public string Currency { get; set; }

        /// <summary>
        /// The Ripple account address of the currency's issuer or gateway, or an empty string if the currency is XRP
        /// </summary>
        /// <remarks><code language="RegExp">^$|^r[1-9A-HJ-NP-Za-km-z]{25,33}$</code></remarks>
        [RegexpPattern("^$|^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("counterparty")]
        public string Counterparty { get; set; }


    }
}

