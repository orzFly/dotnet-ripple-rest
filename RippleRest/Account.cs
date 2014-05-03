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
    /// <summary>
    /// Account helper for RippleRest
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Account's Address (rXXXXXX...)
        /// </summary>
        public String Address { get; set; }

        internal SecureString secret { get; set; }

        /// <summary>
        /// Account's secret. Stored in a <see cref="System.Security.SecureString"/>.
        /// </summary>
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

        /// <summary>
        /// Create a new instance of Account.
        /// </summary>
        /// <param name="address">Account's Address (rXXXXXX...)</param>
        public Account(string address) : this(address, null) { }

        /// <summary>
        /// Create a new instance of Account.
        /// </summary>
        /// <param name="address">Account's Address (rXXXXXX...)</param>
        /// <param name="secret">Account's secret</param>
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

        /// <summary>
        /// Get an account's existing balances.
        /// This includes XRP balance (which does not include a counterparty) and trustline balances.
        /// </summary>
        /// <returns>A list of Balance</returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
        public List<Balance> GetBalances()
        {
            return GetBalances(RippleRestClient.GetDefaultInstanceOrThrow());
        }

        /// <summary>
        /// Get an account's existing balances.
        /// This includes XRP balance (which does not include a counterparty) and trustline balances.
        /// </summary>
        /// <param name="client">A RippleRestClient used for this request.</param>
        /// <returns>A list of Balance</returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
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

        /// <summary>
        /// Returns Trustlines for this account.
        /// </summary>
        /// <returns>A list of Trustline</returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
        public List<Trustline> GetTrustlines()
        {
            return GetTrustlines(RippleRestClient.GetDefaultInstanceOrThrow());
        }

        /// <summary>
        /// Returns Trustlines for this account.
        /// </summary>
        /// <param name="client">A RippleRestClient used for this request.</param>
        /// <returns>A list of Trustline</returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
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

        /// <summary>
        /// Returns AccountSettings for this account.
        /// </summary>
        /// <returns>AccountSettings instance</returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
        public AccountSettings GetSettings()
        {
            return GetSettings(RippleRestClient.GetDefaultInstanceOrThrow());
        }

        /// <summary>
        /// Returns AccountSettings for this account.
        /// </summary>
        /// <param name="client">A RippleRestClient used for this request.</param>
        /// <returns>AccountSettings instance</returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
        public AccountSettings GetSettings(RippleRestClient client)
        {
            var result = client.RestClient.Execute<GetSettingsResult>(client.CreateGetRequest("v1/accounts/{0}/settings", Address));
            client.HandleRestResponseErrors(result);

            result.Data.Settings.Account = this.Address;
            return result.Data.Settings;
        }

        private class GetNotificationResult : RestResultObject
        {
            [JsonProperty("notification")]
            public Notification Notification { set; get; }
        }

        /// <summary>
        /// Get notifications.
        /// 
        /// Clients using notifications to monitor their account activity should pay particular attention to the `state` and `result` fields. The `state` field will either be `validated` or `failed` and represents the finalized status of that transaction. The `result` field will be `tesSUCCESS` if the `state` was validated. If the transaction failed, `result` will contain the `rippled` or `ripple-lib` error code.
        /// 
        /// Notifications have `next_notification_url` and `previous_notification_url`'s. Account notifications can be polled by continuously following the `next_notification_url`, and handling the resultant notifications, until the `next_notification_url` is an empty string. This means that there are no new notifications but, as soon as there are, querying the same URL that produced this notification in the first place will return the same notification but with the `next_notification_url` set.
        /// 
        /// </summary>
        /// <param name="hash">Notification hash</param>
        /// <returns>Notification instance</returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
        public Notification GetNotification(string hash)
        {
            return GetNotification(hash, RippleRestClient.GetDefaultInstanceOrThrow());
        }

        /// <summary>
        /// Get notifications.
        /// 
        /// Clients using notifications to monitor their account activity should pay particular attention to the `state` and `result` fields. The `state` field will either be `validated` or `failed` and represents the finalized status of that transaction. The `result` field will be `tesSUCCESS` if the `state` was validated. If the transaction failed, `result` will contain the `rippled` or `ripple-lib` error code.
        /// 
        /// Notifications have `next_notification_url` and `previous_notification_url`'s. Account notifications can be polled by continuously following the `next_notification_url`, and handling the resultant notifications, until the `next_notification_url` is an empty string. This means that there are no new notifications but, as soon as there are, querying the same URL that produced this notification in the first place will return the same notification but with the `next_notification_url` set.
        /// 
        /// </summary>
        /// <param name="hash">Notification hash</param>
        /// <param name="client">A RippleRestClient used for this request.</param>
        /// <returns>Notification instance</returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
        public Notification GetNotification(string hash, RippleRestClient client)
        {
            var result = client.RestClient.Execute<GetNotificationResult>(client.CreateGetRequest("v1/accounts/{0}/notifications/{1}", Address, hash));
            client.HandleRestResponseErrors(result);

            result.Data.Notification.Account = this.Address;
            return result.Data.Notification;
        }

        private class GetPaymentResult : RestResultObject
        {
            [JsonProperty("payment")]
            public Payment Payment { set; get; }
        }

        /// <summary>
        /// Returns an individual payment.
        /// </summary>
        /// <param name="hash">Payment hash or client resource ID</param>
        /// <returns>Payment instance</returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
        public Payment GetPayment(string hash)
        {
            return GetPayment(hash, RippleRestClient.GetDefaultInstanceOrThrow());
        }

        /// <summary>
        /// Returns an individual payment.
        /// </summary>
        /// <param name="hash">Payment hash or client resource ID</param>
        /// <param name="client">A RippleRestClient used for this request.</param>
        /// <returns>Payment instance</returns>
        /// <exception cref="RippleRestException">Request failed.</exception>
        public Payment GetPayment(string hash, RippleRestClient client)
        {
            var result = client.RestClient.Execute<GetPaymentResult>(client.CreateGetRequest("v1/accounts/{0}/payments/{1}", Address, hash));
            client.HandleRestResponseErrors(result);

            return result.Data.Payment;
        }
    }
}
