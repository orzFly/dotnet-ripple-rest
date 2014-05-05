using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    /// <summary>
    /// A simplified Trustline object used by the ripple-rest API
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public partial class Trustline : RestObject
    {
        /// <summary>
        /// The account from whose perspective this trustline is being viewed
        /// </summary>
        /// <remarks><code language="RegExp">^r[1-9A-HJ-NP-Za-km-z]{25,33}$</code></remarks>
        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("account", Required = Required.Always)]
        public string Account { get; set; }

        /// <summary>
        /// The other party in this trustline
        /// </summary>
        /// <remarks><code language="RegExp">^r[1-9A-HJ-NP-Za-km-z]{25,33}$</code></remarks>
        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("counterparty")]
        public string Counterparty { get; set; }

        /// <summary>
        /// The code of the currency in which this trustline denotes trust
        /// </summary>
        /// <remarks><code language="RegExp">^([a-zA-Z0-9]{3}|[A-Fa-f0-9]{40})$</code></remarks>
        [RegexpPattern("^([a-zA-Z0-9]{3}|[A-Fa-f0-9]{40})$")]
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// The maximum value of the currency that the account may hold issued by the counterparty
        /// </summary>
        /// <remarks><code language="RegExp">^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$</code></remarks>
        [RegexpPattern("^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$")]
        [JsonProperty("limit", Required = Required.Always)]
        public string Limit { get; set; }

        /// <summary>
        /// The maximum value of the currency that the counterparty may hold issued by the account
        /// </summary>
        /// <remarks><code language="RegExp">^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$</code></remarks>
        [RegexpPattern("^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$")]
        [JsonProperty("reciprocated_limit")]
        public string ReciprocatedLimit { get; set; }

        /// <summary>
        /// Set to true if the account has explicitly authorized the counterparty to hold currency it issues. This is only necessary if the account's settings include require_authorization_for_incoming_trustlines
        /// </summary>
        [JsonProperty("authorized_by_account")]
        public bool AuthorizedByAccount { get; set; }

        /// <summary>
        /// Set to true if the counterparty has explicitly authorized the account to hold currency it issues. This is only necessary if the counterparty's settings include require_authorization_for_incoming_trustlines
        /// </summary>
        [JsonProperty("authorized_by_counterparty")]
        public bool AuthorizedByCounterparty { get; set; }

        /// <summary>
        /// If true it indicates that the account allows pairwise rippling out through this trustline
        /// </summary>
        [JsonProperty("account_allows_rippling")]
        public bool AccountAllowsRippling { get; set; }

        /// <summary>
        /// If true it indicates that the counterparty allows pairwise rippling out through this trustline
        /// </summary>
        [JsonProperty("counterparty_allows_rippling")]
        public bool CounterpartyAllowsRippling { get; set; }

        /// <summary>
        /// The string representation of the index number of the ledger containing this trustline or, in the case of historical queries, of the transaction that modified this Trustline
        /// </summary>
        /// <remarks><code language="RegExp">^[0-9]+$</code></remarks>
        [RegexpPattern("^[0-9]+$")]
        [JsonProperty("ledger")]
        public string Ledger { get; set; }

        /// <summary>
        /// If this object was returned by a historical query this value will be the hash of the transaction that modified this Trustline. The transaction hash is used throughout the Ripple Protocol to uniquely identify a particular transaction
        /// </summary>
        /// <remarks><code language="RegExp">^$|^[A-Fa-f0-9]{64}$</code></remarks>
        [RegexpPattern("^$|^[A-Fa-f0-9]{64}$")]
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// If the trustline was changed this will be a full Trustline object representing the previous values. If the previous object also had a previous object that will be removed to reduce data complexity. Trustline changes can be walked backwards by querying the API for previous.hash repeatedly
        /// </summary>
        [JsonProperty("previous")]
        public Trustline Previous { get; set; }


    }
}

