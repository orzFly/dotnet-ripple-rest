using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    /// <summary>
    /// An object 
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public partial class AccountSettings : RestObject
    {
        /// <summary>
        /// The Ripple address of the account in question
        /// </summary>
        /// <remarks><code language="RegExp">^r[1-9A-HJ-NP-Za-km-z]{25,33}$</code></remarks>
        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("account", Required = Required.Always)]
        public string Account { get; set; }

        /// <summary>
        /// The hash of an optional additional public key that can be used for signing and verifying transactions
        /// </summary>
        /// <remarks><code language="RegExp">^r[1-9A-HJ-NP-Za-km-z]{25,33}$</code></remarks>
        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("regular_key")]
        public string RegularKey { get; set; }

        /// <summary>
        /// The domain associated with this account. The ripple.txt file can be looked up to verify this information
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// The MD5 128-bit hash of the account owner's email address
        /// </summary>
        /// <remarks><code language="RegExp">^$|^[A-Fa-f0-9]{32}$</code></remarks>
        [RegexpPattern("^$|^[A-Fa-f0-9]{32}$")]
        [JsonProperty("email_hash")]
        public string EmailHash { get; set; }

        /// <summary>
        /// An optional public key, represented as hex, that can be set to allow others to send encrypted messages to the account owner
        /// </summary>
        /// <remarks><code language="RegExp">^([0-9a-fA-F]{2}){0,33}$</code></remarks>
        [RegexpPattern("^([0-9a-fA-F]{2}){0,33}$")]
        [JsonProperty("message_key")]
        public string MessageKey { get; set; }

        /// <summary>
        /// A number representation of the rate charged each time a holder of currency issued by this account transfers it. By default the rate is 100. A rate of 101 is a 1% charge on top of the amount being transferred. Up to nine decimal places are supported
        /// </summary>
        [JsonProperty("transfer_rate")]
        public double TransferRate { get; set; }

        /// <summary>
        /// If set to true incoming payments will only be validated if they include a destination_tag. This may be used primarily by gateways that operate exclusively with hosted wallets
        /// </summary>
        [JsonProperty("require_destination_tag")]
        public bool RequireDestinationTag { get; set; }

        /// <summary>
        /// If set to true incoming trustlines will only be validated if this account first creates a trustline to the counterparty with the authorized flag set to true. This may be used by gateways to prevent accounts unknown to them from holding currencies they issue
        /// </summary>
        [JsonProperty("require_authorization")]
        public bool RequireAuthorization { get; set; }

        /// <summary>
        /// If set to true incoming XRP payments will be allowed
        /// </summary>
        [JsonProperty("disallow_xrp")]
        public bool DisallowXRP { get; set; }

        /// <summary>
        /// A string representation of the last sequence number of a validated transaction created by this account
        /// </summary>
        [JsonProperty("transaction_sequence")]
        public UInt32 TransactionSequence { get; set; }

        /// <summary>
        /// The number of trustlines owned by this account. This value does not include incoming trustlines where this account has not explicitly reciprocated trust
        /// </summary>
        [JsonProperty("trustline_count")]
        public UInt32 TrustlineCount { get; set; }

        /// <summary>
        /// The string representation of the index number of the ledger containing these account settings or, in the case of historical queries, of the transaction that modified these settings
        /// </summary>
        /// <remarks><code language="RegExp">^[0-9]+$</code></remarks>
        [RegexpPattern("^[0-9]+$")]
        [JsonProperty("ledger")]
        public string Ledger { get; set; }

        /// <summary>
        /// If this object was returned by a historical query this value will be the hash of the transaction that modified these settings. The transaction hash is used throughout the Ripple Protocol to uniquely identify a particular transaction
        /// </summary>
        /// <remarks><code language="RegExp">^$|^[A-Fa-f0-9]{64}$</code></remarks>
        [RegexpPattern("^$|^[A-Fa-f0-9]{64}$")]
        [JsonProperty("hash")]
        public string Hash { get; set; }


    }
}

