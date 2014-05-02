using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public partial class Balance : RestObject
    {
        [JsonProperty("value", Required = Required.Always)]
        public string Value { get; set; }

        [RegexpPattern("^([a-zA-Z0-9]{3}|[A-Fa-f0-9]{40})$")]
        [JsonProperty("currency", Required = Required.Always)]
        public string Currency { get; set; }

        [RegexpPattern("^$|^r[1-9A-HJ-NP-Za-km-z]{25,33}$")]
        [JsonProperty("counterparty")]
        public string Counterparty { get; set; }


    }
}

