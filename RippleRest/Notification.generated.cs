using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    /// <summary>
    /// A 
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public partial class Notification : RestObject
    {
        /// <summary>
        /// The Ripple address of the account to which the notification pertains
        /// </summary>
        /// <remarks><code language="RegExp">^r[1-9A-HJ-NP-Za-km-z]{25,33}$</code></remarks>
        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("account")]
        public string Account { get; set; }

        /// <summary>
        /// The resource type this notification corresponds to. Possible values are "payment", "order", "trustline", "accountsettings"
        /// </summary>
        /// <remarks><code language="RegExp">^payment|order|trustline|accountsettings$</code></remarks>
        [RegexpPattern("^payment|order|trustline|accountsettings$")]
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The direction of the transaction, from the perspective of the account being queried. Possible values are "incoming", "outgoing", and "passthrough"
        /// </summary>
        /// <remarks><code language="RegExp">^incoming|outgoing|passthrough$</code></remarks>
        [RegexpPattern("^incoming|outgoing|passthrough$")]
        [JsonProperty("direction")]
        public string Direction { get; set; }

        /// <summary>
        /// The state of the transaction from the perspective of the Ripple Ledger. Possible values are "validated" and "failed"
        /// </summary>
        /// <remarks><code language="RegExp">^validated|failed$</code></remarks>
        [RegexpPattern("^validated|failed$")]
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// The rippled code indicating the success or failure type of the transaction. The code "tesSUCCESS" indicates that the transaction was successfully validated and written into the Ripple Ledger. All other codes will begin with the following prefixes: "tec", "tef", "tel", or "tej"
        /// </summary>
        /// <remarks><code language="RegExp">te[cfjlms][A-Za-z_]+</code></remarks>
        [RegexpPattern("te[cfjlms][A-Za-z_]+")]
        [JsonProperty("result")]
        public string Result { get; set; }

        /// <summary>
        /// The string representation of the index number of the ledger containing the validated or failed transaction. Failed payments will only be written into the Ripple Ledger if they fail after submission to a rippled and a Ripple Network fee is claimed
        /// </summary>
        /// <remarks><code language="RegExp">^[0-9]+$</code></remarks>
        [RegexpPattern("^[0-9]+$")]
        [JsonProperty("ledger")]
        public string Ledger { get; set; }

        /// <summary>
        /// The 256-bit hash of the transaction. This is used throughout the Ripple protocol as the unique identifier for the transaction
        /// </summary>
        /// <remarks><code language="RegExp">^$|^[A-Fa-f0-9]{64}$</code></remarks>
        [RegexpPattern("^$|^[A-Fa-f0-9]{64}$")]
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// The timestamp representing when the transaction was validated and written into the Ripple ledger
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// A URL that can be used to fetch the full resource this notification corresponds to
        /// </summary>
        [JsonProperty("transaction_url")]
        public string TransactionUrl { get; set; }

        /// <summary>
        /// A URL that can be used to fetch the notification that preceded this one chronologically
        /// </summary>
        [JsonProperty("previous_notification_url")]
        public string PreviousNotificationUrl { get; set; }

        /// <summary>
        /// A URL that can be used to fetch the notification that followed this one chronologically
        /// </summary>
        [JsonProperty("next_notification_url")]
        public string NextNotificationUrl { get; set; }


    }
}

