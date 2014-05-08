using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Newtonsoft.Json;

namespace RippleRest
{
    /// <summary>
    /// https://github.com/ripple/ripple-rest/blob/develop/docs/api-reference.md#get-server-info
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Transaction : RestResponseObject
    {
        [Serializable]
        [TypeConverter(typeof(SerializableExpandableObjectConverter))]
        public class FinalFields
        {
            public string Account { get; set; }
            public string Balance { get; set; }
            public int Flags { get; set; }
            public int OwnerCount { get; set; }
            public int Sequence { get; set; }
        }

        [Serializable]
        [TypeConverter(typeof(SerializableExpandableObjectConverter))]
        public class PreviousFields
        {
            public string Balance { get; set; }
            public int? Sequence { get; set; }
        }

        [Serializable]
        [TypeConverter(typeof(SerializableExpandableObjectConverter))]
        public class ModifiedNode
        {
            public FinalFields FinalFields { get; set; }
            public string LedgerEntryType { get; set; }
            public string LedgerIndex { get; set; }
            public PreviousFields PreviousFields { get; set; }
            public string PreviousTxnID { get; set; }
            public int PreviousTxnLgrSeq { get; set; }
        }

        [Serializable]
        [TypeConverter(typeof(SerializableExpandableObjectConverter))]
        public class AffectedNode
        {
            public ModifiedNode ModifiedNode { get; set; }
        }

        [Serializable]
        [TypeConverter(typeof(SerializableExpandableObjectConverter))]
        public class Metas
        {
            public List<AffectedNode> AffectedNodes { get; set; }
            public int TransactionIndex { get; set; }
            public string TransactionResult { get; set; }
        }

        public string Account { get; set; }

        public string Amount { get; set; }

        public string Destination { get; set; }

        public string Fee { get; set; }

        public int Flags { get; set; }

        public int Sequence { get; set; }

        public string SigningPubKey { get; set; }

        public string TransactionType { get; set; }

        public string TxnSignature { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("inLedger")]
        public int InLedger { get; set; }

        [JsonProperty("ledger_index")]
        public int LedgerIndex { get; set; }

        [JsonProperty("meta")]
        public Metas Meta { get; set; }

        [JsonProperty("validated")]
        public bool Validated { get; set; }

        [JsonProperty("date")]
        public long Date { get; set; }
    }
}
