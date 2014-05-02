using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;
using RestSharp;
using Newtonsoft.Json;

namespace RippleRest
{
    public class Account
    {
        public String Address { get; set; }

        protected SecureString secret { get; set; }

        public String Secret {
            set
            {
                if (value == null)
                {
                    this.secret = null;
                    return;
                }

                this.secret = new SecureString();
                foreach(char c in value)
                {
                    this.secret.AppendChar(c);
                }
                this.secret.MakeReadOnly();
            }

            internal get
            {
                if (secret == null)
                    return null;

                IntPtr unmanagedString = IntPtr.Zero;
                try
                {
                    unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secret);
                    return Marshal.PtrToStringUni(unmanagedString);
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
                }
            }
        }

        public Account(string address) : this(address, null) { }
        public Account(string address, string secret)
        {
            this.Address = address;
            this.Secret = secret;
        }

        private class GetBalancesResult : RestResultObject
        {
            [JsonProperty("balances")]
            public List<Balance> Balances { set; get; }
        }

        public List<Balance> GetBalances()
        {
            return GetBalances(RippleRestClient.GetDefaultInstanceOrThrow());
        }

        public List<Balance> GetBalances(RippleRestClient client)
        {
            var result = client.RestClient.Execute<GetBalancesResult>(client.CreateGetRequest("v1/accounts/{0}/balances", Address));
            client.HandleRestResponseErrors(result);

            return result.Data.Balances;
        }

        private class GetTrustlinesResult : RestResultObject
        {
            [JsonProperty("trustlines")]
            public List<Trustline> Trustlines { set; get; }
        }

        public List<Trustline> GetTrustlines()
        {
            return GetTrustlines(RippleRestClient.GetDefaultInstanceOrThrow());
        }

        public List<Trustline> GetTrustlines(RippleRestClient client)
        {
            var result = client.RestClient.Execute<GetTrustlinesResult>(client.CreateGetRequest("v1/accounts/{0}/trustlines", Address));
            client.HandleRestResponseErrors(result);

            return result.Data.Trustlines;
        }

        private class GetSettingsResult : RestResultObject
        {
            [JsonProperty("settings")]
            public AccountSettings Settings { set; get; }
        }

        public AccountSettings GetSettings()
        {
            return GetSettings(RippleRestClient.GetDefaultInstanceOrThrow());
        }

        public AccountSettings GetSettings(RippleRestClient client)
        {
            var result = client.RestClient.Execute<GetSettingsResult>(client.CreateGetRequest("v1/accounts/{0}/settings", Address));
            client.HandleRestResponseErrors(result);

            result.Data.Settings.Account = this.Address;
            return result.Data.Settings;
        }
    }
}
