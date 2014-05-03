using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    /// <summary>
    /// A simplified Order object used by the ripple-rest API (note that "orders" are referred to elsewhere in the Ripple protocol as "offers")
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public partial class Order : RestObject
    {
        /// <summary>
        /// The Ripple account address of the order's creator
        /// </summary>
        /// <remarks><code language="RegExp">^r[1-9A-HJ-NP-Za-km-z]{25,33}$</code></remarks>
        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("account", Required = Required.Always)]
        public string Account { get; set; }

        /// <summary>
        /// If set to true the order it indicates that the creator is looking to receive the base_amount in exchange for the counter_amount. If undefined or set to false it indicates that the creator is looking to sell the base_amount to receive the counter_amount
        /// </summary>
        [JsonProperty("buy")]
        public bool Buy { get; set; }

        /// <summary>
        /// The amount of currency the seller_account is seeking to buy. If other orders take part of this one, this value will change to represent the amount left in the order. This may be specified along with the counter_amount OR exchange_rate but not both. When the order is parsed from the Ripple Ledger the base currency will be determined according to the Priority Ranking of Currencies (XRP,EUR,GBP,AUD,NZD,USD,CAD,CHF,JPY,CNY) and if neither currency is listed in the ranking the base currency will be the one that is alphabetically first
        /// </summary>
        [JsonProperty("base_amount")]
        public Amount BaseAmount { get; set; }

        /// <summary>
        /// The amount of currency being sold. If other orders take part of this one, this value will change to represent the amount left in the order. This may be specified along with the base_amount OR the exchange_rate but not both
        /// </summary>
        [JsonProperty("counter_amount")]
        public Amount CounterAmount { get; set; }

        /// <summary>
        /// A string representation of the order price, defined as the cost one unit of the base currency in terms of the counter currency. This may be specified along with the base_amount OR the counter_amount but not both. If it is unspecified it will be computed automatically based on the counter_amount divided by the base_amount
        /// </summary>
        /// <remarks><code language="RegExp">^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$</code></remarks>
        [RegexpPattern("^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$")]
        [JsonProperty("exchange_rate")]
        public string ExchangeRate { get; set; }

        /// <summary>
        /// The ISO combined date and time string representing the point beyond which the order will no longer be considered active or valid
        /// </summary>
        [JsonProperty("expiration_timestamp")]
        public DateTime ExpirationTimestamp { get; set; }

        /// <summary>
        /// A string representation of the number of ledger closes after submission during which the order should be considered active
        /// </summary>
        /// <remarks><code language="RegExp">^[0-9]*$</code></remarks>
        [RegexpPattern("^[0-9]*$")]
        [JsonProperty("ledger_timeout")]
        public string LedgerTimeout { get; set; }

        /// <summary>
        /// If set to true this order will only take orders that are available at the time of execution and will not create an entry in the Ripple Ledger
        /// </summary>
        [JsonProperty("immediate_or_cancel")]
        public bool ImmediateOrCancel { get; set; }

        /// <summary>
        /// If set to true this order will only take orders that fill the base_amount and are available at the time of execution and will not create an entry in the Ripple Ledger
        /// </summary>
        [JsonProperty("fill_or_kill")]
        public bool FillOrKill { get; set; }

        /// <summary>
        /// If set to true and it is a buy order it will buy up to the base_amount even if the counter_amount is exceeded, if it is a sell order it will sell up to the counter_amount even if the base_amount is exceeded
        /// </summary>
        [JsonProperty("maximize_buy_or_sell")]
        public bool MaximizeBuyOrSell { get; set; }

        /// <summary>
        /// If this is set to the sequence number of an outstanding order, that order will be cancelled and replaced with this one
        /// </summary>
        /// <remarks><code language="RegExp">^d*$</code></remarks>
        [RegexpPattern("^d*$")]
        [JsonProperty("cancel_replace")]
        public string CancelReplace { get; set; }

        /// <summary>
        /// The sequence number of this order from the perspective of the seller_account. The seller_account and the sequence number uniquely identify the order in the Ripple Ledger
        /// </summary>
        /// <remarks><code language="RegExp">^[0-9]*$</code></remarks>
        [RegexpPattern("^[0-9]*$")]
        [JsonProperty("sequence")]
        public string Sequence { get; set; }

        /// <summary>
        /// The Ripple Network transaction fee, represented in whole XRP (NOT "drops", or millionths of an XRP, which is used elsewhere in the Ripple protocol) used to create the order
        /// </summary>
        /// <remarks><code language="RegExp">^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$</code></remarks>
        [RegexpPattern("^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$")]
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// If the order is active the state will be "active". If this object represents a historical order the state will be "validated", "filled" if the order was removed because it was fully filled, "cancelled" if it was deleted by the owner, "expired" if it reached the expiration_timestamp, or "failed" if there was an error with the initial attempt to place the order
        /// </summary>
        /// <remarks><code language="RegExp">^active|validated|filled|cancelled|expired|failed$</code></remarks>
        [RegexpPattern("^active|validated|filled|cancelled|expired|failed$")]
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// The string representation of the index number of the ledger containing this order or, in the case of historical queries, of the transaction that modified this Order. 
        /// </summary>
        /// <remarks><code language="RegExp">^[0-9]+$</code></remarks>
        [RegexpPattern("^[0-9]+$")]
        [JsonProperty("ledger")]
        public string Ledger { get; set; }

        /// <summary>
        /// When returned as the result of a historical query this will be the hash of Ripple transaction that created, modified, or deleted this order. The transaction hash is used throughout the Ripple Protocol to uniquely identify a particular transaction
        /// </summary>
        /// <remarks><code language="RegExp">^$|^[A-Fa-f0-9]{64}$</code></remarks>
        [RegexpPattern("^$|^[A-Fa-f0-9]{64}$")]
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// If the order was modified or partially filled this will be a full Order object. If the previous object also had a previous object that will be removed to reduce data complexity. Order changes can be walked backwards by querying the API for previous.hash repeatedly
        /// </summary>
        [JsonProperty("previous")]
        public Order Previous { get; set; }


    }
}

