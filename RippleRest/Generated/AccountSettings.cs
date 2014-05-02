using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public partial class AccountSettings : RestObject
    {
        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("account", Required = Required.Always)]
        public string Account { get; set; }

        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("regular_key")]
        public string RegularKey { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [RegexpPattern("^$|^[A-Fa-f0-9]{32}$")]
        [JsonProperty("email_hash")]
        public string EmailHash { get; set; }

        [RegexpPattern("^([0-9a-fA-F]{2}){0,33}$")]
        [JsonProperty("message_key")]
        public string MessageKey { get; set; }

        [JsonProperty("transfer_rate")]
        public double TransferRate { get; set; }

        [JsonProperty("require_destination_tag")]
        public bool RequireDestinationTag { get; set; }

        [JsonProperty("require_authorization")]
        public bool RequireAuthorization { get; set; }

        [JsonProperty("disallow_xrp")]
        public bool DisallowXRP { get; set; }

        [JsonProperty("transaction_sequence")]
        public UInt32 TransactionSequence { get; set; }

        [JsonProperty("trustline_count")]
        public UInt32 TrustlineCount { get; set; }

        [RegexpPattern("^[0-9]+$")]
        [JsonProperty("ledger")]
        public string Ledger { get; set; }

        [RegexpPattern("^$|^[A-Fa-f0-9]{64}$")]
        [JsonProperty("hash")]
        public string Hash { get; set; }


    }
}

