using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public partial class Trustline : RestObject
    {
        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("account", Required = Required.Always)]
        public string Account { get; set; }

        [RegexpPattern("^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("counterparty")]
        public string Counterparty { get; set; }

        [RegexpPattern("^([a-zA-Z0-9]{3}|[A-Fa-f0-9]{40})$")]
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [RegexpPattern("^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$")]
        [JsonProperty("limit", Required = Required.Always)]
        public string Limit { get; set; }

        [RegexpPattern("^[-+]?[0-9]*[.]?[0-9]+([eE][-+]?[0-9]+)?$")]
        [JsonProperty("reciprocated_limit")]
        public string ReciprocatedLimit { get; set; }

        [JsonProperty("authorized_by_account")]
        public bool AuthorizedByAccount { get; set; }

        [JsonProperty("authorized_by_counterparty")]
        public bool AuthorizedByCounterparty { get; set; }

        [JsonProperty("account_allows_rippling")]
        public bool AccountAllowsRippling { get; set; }

        [JsonProperty("counterparty_allows_rippling")]
        public bool CounterpartyAllowsRippling { get; set; }

        [RegexpPattern("^[0-9]+$")]
        [JsonProperty("ledger")]
        public string Ledger { get; set; }

        [RegexpPattern("^$|^[A-Fa-f0-9]{64}$")]
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("previous")]
        public Trustline Previous { get; set; }


    }
}

