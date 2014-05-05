using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RippleRest
{
    public partial class Amount
    {
        /// <summary>
        /// Convert to a Ripple Rest Currency String
        /// </summary>
        /// <returns>XRP or CNY+rXXXXX</returns>
        public string ToCurrencyString()
        {
            if (String.IsNullOrEmpty(this.Counterparty))
                return this.Currency;
            else
                return String.Format("{0}+{1}", this.Currency, this.Counterparty);
        }

        /// <summary>
        /// Convert to a Ripple Rest Amount String
        /// </summary>
        /// <returns>3+XRP or 3+CNY+rXXXXX</returns>
        public override string ToString()
        {
            return String.Format("{0}+{1}", this.Value, this.ToCurrencyString());
        }
    }
}
