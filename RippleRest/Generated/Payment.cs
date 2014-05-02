using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public partial class Payment : RestObject
    {
        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("source_account", Required = Required.Always)]
        public string SourceAccount { get; set; }

        [JsonProperty("source_tag")]
        public UInt32 SourceTag { get; set; }

        [JsonProperty("source_amount")]
        public Amount SourceAmount { get; set; }

        [RegexpPattern("^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$")]
        [JsonProperty("source_slippage")]
        public string SourceSlippage { get; set; }

        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("destination_account", Required = Required.Always)]
        public string DestinationAccount { get; set; }

        [JsonProperty("destination_tag")]
        public UInt32 DestinationTag { get; set; }

        [JsonProperty("destination_amount", Required = Required.Always)]
        public Amount DestinationAmount { get; set; }

        [RegexpPattern("^$|^[A-Fa-f0-9]{64}$")]
        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        [JsonProperty("paths")]
        public string Paths { get; set; }

        [JsonProperty("partial_payment")]
        public bool PartialPayment { get; set; }

        [JsonProperty("no_direct_ripple")]
        public bool NoDirectRipple { get; set; }

        [RegexpPattern("^incoming|outgoing|passthrough$")]
        [JsonProperty("direction")]
        public string Direction { get; set; }

        [RegexpPattern("^validated|failed|new$")]
        [JsonProperty("state")]
        public string State { get; set; }

        [RegexpPattern("te[cfjlms][A-Za-z_]+")]
        [JsonProperty("result")]
        public string Result { get; set; }

        [RegexpPattern("^[0-9]+$")]
        [JsonProperty("ledger")]
        public string Ledger { get; set; }

        [RegexpPattern("^$|^[A-Fa-f0-9]{64}$")]
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [RegexpPattern("^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$")]
        [JsonProperty("fee")]
        public string Fee { get; set; }

        [JsonProperty("source_balance_changes")]
        public Amount[] SourceBalanceChanges { get; set; }

        [JsonProperty("destination_balance_changes")]
        public Amount[] DestinationBalanceChanges { get; set; }


    }
}

