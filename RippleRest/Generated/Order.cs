using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public partial class Order : RestObject
    {
        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("account", Required = Required.Always)]
        public string Account { get; set; }

        [JsonProperty("buy")]
        public bool Buy { get; set; }

        [JsonProperty("base_amount")]
        public Amount BaseAmount { get; set; }

        [JsonProperty("counter_amount")]
        public Amount CounterAmount { get; set; }

        [RegexpPattern("^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$")]
        [JsonProperty("exchange_rate")]
        public string ExchangeRate { get; set; }

        [JsonProperty("expiration_timestamp")]
        public DateTime ExpirationTimestamp { get; set; }

        [RegexpPattern("^[0-9]*$")]
        [JsonProperty("ledger_timeout")]
        public string LedgerTimeout { get; set; }

        [JsonProperty("immediate_or_cancel")]
        public bool ImmediateOrCancel { get; set; }

        [JsonProperty("fill_or_kill")]
        public bool FillOrKill { get; set; }

        [JsonProperty("maximize_buy_or_sell")]
        public bool MaximizeBuyOrSell { get; set; }

        [RegexpPattern("^d*$")]
        [JsonProperty("cancel_replace")]
        public string CancelReplace { get; set; }

        [RegexpPattern("^[0-9]*$")]
        [JsonProperty("sequence")]
        public string Sequence { get; set; }

        [RegexpPattern("^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$")]
        [JsonProperty("fee")]
        public string Fee { get; set; }

        [RegexpPattern("^active|validated|filled|cancelled|expired|failed$")]
        [JsonProperty("state")]
        public string State { get; set; }

        [RegexpPattern("^[0-9]+$")]
        [JsonProperty("ledger")]
        public string Ledger { get; set; }

        [RegexpPattern("^$|^[A-Fa-f0-9]{64}$")]
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("previous")]
        public Order Previous { get; set; }


    }
}

