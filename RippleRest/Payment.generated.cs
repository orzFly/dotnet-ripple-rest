using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    /// <summary>
    /// A flattened Payment object used by the ripple-rest API
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public partial class Payment : RestObject
    {
        /// <summary>
        /// The Ripple account address of the Payment sender
        /// </summary>
        /// <remarks><code language="RegExp">^r[1-9A-HJ-NP-Za-km-z]{25,33}$</code></remarks>
        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("source_account", Required = Required.Always)]
        public string SourceAccount { get; set; }

        /// <summary>
        /// A string representing an unsigned 32-bit integer most commonly used to refer to a sender's hosted account at a Ripple gateway
        /// </summary>
        [JsonProperty("source_tag")]
        public string SourceTag { get; set; }

        /// <summary>
        /// An optional amount that can be specified to constrain cross-currency payments
        /// </summary>
        [JsonProperty("source_amount")]
        public Amount SourceAmount { get; set; }

        /// <summary>
        /// An optional cushion for the source_amount to increase the likelihood that the payment will succeed. The source_account will never be charged more than source_amount.value + source_slippage
        /// </summary>
        /// <remarks><code language="RegExp">^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$</code></remarks>
        [RegexpPattern("^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$")]
        [JsonProperty("source_slippage")]
        public string SourceSlippage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks><code language="RegExp">^r[1-9A-HJ-NP-Za-km-z]{25,33}$</code></remarks>
        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("destination_account", Required = Required.Always)]
        public string DestinationAccount { get; set; }

        /// <summary>
        /// A string representing an unsigned 32-bit integer most commonly used to refer to a receiver's hosted account at a Ripple gateway
        /// </summary>
        [JsonProperty("destination_tag")]
        public string DestinationTag { get; set; }

        /// <summary>
        /// The amount the destination_account will receive
        /// </summary>
        [JsonProperty("destination_amount", Required = Required.Always)]
        public Amount DestinationAmount { get; set; }

        /// <summary>
        /// A 256-bit hash that can be used to identify a particular payment
        /// </summary>
        /// <remarks><code language="RegExp">^$|^[A-Fa-f0-9]{64}$</code></remarks>
        [RegexpPattern("^$|^[A-Fa-f0-9]{64}$")]
        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        /// <summary>
        /// A "stringified" version of the Ripple PathSet structure that users should treat as opaque
        /// </summary>
        [JsonProperty("paths")]
        public string Paths { get; set; }

        /// <summary>
        /// A boolean that, if set to true, indicates that this payment should go through even if the whole amount cannot be delivered because of a lack of liquidity or funds in the source_account account
        /// </summary>
        [JsonProperty("partial_payment")]
        public bool PartialPayment { get; set; }

        /// <summary>
        /// A boolean that can be set to true if paths are specified and the sender would like the Ripple Network to disregard any direct paths from the source_account to the destination_account. This may be used to take advantage of an arbitrage opportunity or by gateways wishing to issue balances from a hot wallet to a user who has mistakenly set a trustline directly to the hot wallet
        /// </summary>
        [JsonProperty("no_direct_ripple")]
        public bool NoDirectRipple { get; set; }

        /// <summary>
        /// The direction of the payment, from the perspective of the account being queried. Possible values are "incoming", "outgoing", and "passthrough"
        /// </summary>
        /// <remarks><code language="RegExp">^incoming|outgoing|passthrough$</code></remarks>
        [RegexpPattern("^incoming|outgoing|passthrough$")]
        [JsonProperty("direction")]
        public string Direction { get; set; }

        /// <summary>
        /// The state of the payment from the perspective of the Ripple Ledger. Possible values are "validated" and "failed" and "new" if the payment has not been submitted yet
        /// </summary>
        /// <remarks><code language="RegExp">^validated|failed|new$</code></remarks>
        [RegexpPattern("^validated|failed|new$")]
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// The rippled code indicating the success or failure type of the payment. The code "tesSUCCESS" indicates that the payment was successfully validated and written into the Ripple Ledger. All other codes will begin with the following prefixes: "tec", "tef", "tel", or "tej"
        /// </summary>
        /// <remarks><code language="RegExp">te[cfjlms][A-Za-z_]+</code></remarks>
        [RegexpPattern("te[cfjlms][A-Za-z_]+")]
        [JsonProperty("result")]
        public string Result { get; set; }

        /// <summary>
        /// The string representation of the index number of the ledger containing the validated or failed payment. Failed payments will only be written into the Ripple Ledger if they fail after submission to a rippled and a Ripple Network fee is claimed
        /// </summary>
        /// <remarks><code language="RegExp">^[0-9]+$</code></remarks>
        [RegexpPattern("^[0-9]+$")]
        [JsonProperty("ledger")]
        public string Ledger { get; set; }

        /// <summary>
        /// The 256-bit hash of the payment. This is used throughout the Ripple protocol as the unique identifier for the transaction
        /// </summary>
        /// <remarks><code language="RegExp">^$|^[A-Fa-f0-9]{64}$</code></remarks>
        [RegexpPattern("^$|^[A-Fa-f0-9]{64}$")]
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// The timestamp representing when the payment was validated and written into the Ripple ledger
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
        
        /// <summary>
        /// The Ripple Network transaction fee, represented in whole XRP (NOT "drops", or millionths of an XRP, which is used elsewhere in the Ripple protocol)
        /// </summary>
        /// <remarks><code language="RegExp">^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$</code></remarks>
        [RegexpPattern("^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$")]
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// Parsed from the validated transaction metadata, this array represents all of the changes to balances held by the source_account. Most often this will have one amount representing the Ripple Network fee and, if the source_amount was not XRP, one amount representing the actual source_amount that was sent
        /// </summary>
        [JsonProperty("source_balance_changes")]
        public List<Amount> SourceBalanceChanges { get; set; }

        /// <summary>
        /// Parsed from the validated transaction metadata, this array represents the changes to balances held by the destination_account. For those receiving payments this is important to check because if the partial_payment flag is set this value may be less than the destination_amount
        /// </summary>
        [JsonProperty("destination_balance_changes")]
        public List<Amount> DestinationBalanceChanges { get; set; }


    }
}

