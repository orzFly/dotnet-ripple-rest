using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public partial class Notification : RestObject
    {
        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("account")]
        public string Account { get; set; }

        [RegexpPattern("^payment|order|trustline|accountsettings$")]
        [JsonProperty("type")]
        public string Type { get; set; }

        [RegexpPattern("^incoming|outgoing|passthrough$")]
        [JsonProperty("direction")]
        public string Direction { get; set; }

        [RegexpPattern("^validated|failed$")]
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

        [JsonProperty("transaction_url")]
        public string TransactionUrl { get; set; }

        [JsonProperty("previous_notification_url")]
        public string PreviousNotificationUrl { get; set; }

        [JsonProperty("next_notification_url")]
        public string NextNotificationUrl { get; set; }


    }
}

