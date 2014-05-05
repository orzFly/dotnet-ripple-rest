using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    /// <summary>
    /// https://github.com/ripple/ripple-rest/blob/develop/docs/api-reference.md#get-server-info
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class ServerInfo : RestResponseObject
    {
        [Serializable]
        [TypeConverter(typeof(SerializableExpandableObjectConverter))]
        public class LastClose
        {
            [JsonProperty("converge_time_s")]
            public double ConvergeTimeS { get; set; }

            [JsonProperty("proposers")]
            public int Proposers { get; set; }
        }

        [Serializable]
        [TypeConverter(typeof(SerializableExpandableObjectConverter))]
        public class ValidatedLedger
        {
            [JsonProperty("age")]
            public int Age { get; set; }

            [JsonProperty("base_fee_xrp")]
            public double BaseFeeXRP { get; set; }

            [JsonProperty("hash")]
            public string Hash { get; set; }

            [JsonProperty("reserve_base_xrp")]
            public int ReserveBaseXRP { get; set; }

            [JsonProperty("reserve_inc_xrp")]
            public int ReserveIncXRP { get; set; }

            [JsonProperty("seq")]
            public int Sequence { get; set; }
        }

        [Serializable]
        [TypeConverter(typeof(SerializableExpandableObjectConverter))]
        public class RippledStatus
        {
            [JsonProperty("build_version")]
            public string BuildVersion { get; set; }

            [JsonProperty("complete_ledgers")]
            public string CompleteLedgers { get; set; }

            [JsonProperty("hostid")]
            public string HostID { get; set; }

            [JsonProperty("last_close")]
            public LastClose LastClose { get; set; }

            [JsonProperty("load_factor")]
            public int LoadFactor { get; set; }

            [JsonProperty("peers")]
            public int Peers { get; set; }

            [JsonProperty("pubkey_node")]
            public string PubkeyNode { get; set; }

            [JsonProperty("server_state")]
            public string ServerState { get; set; }

            [JsonProperty("validated_ledger")]
            public ValidatedLedger ValidatedLedger { get; set; }

            [JsonProperty("validation_quorum")]
            public int ValidationQuorum { get; set; }
        }

        [JsonProperty("rippled_server_url")]
        public string RippledServerURL { get; set; }

        [JsonProperty("rippled_server_status")]
        public RippledStatus RippledServerStatus { get; set; }

        [JsonProperty("api_documentation_url")]
        public string APIDocumentationURL { get; set; }
    }
}
