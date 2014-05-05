using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.ComponentModel;

namespace RippleRest
{
    /// <summary>
    /// Base class for all RestObject
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public abstract class RestObject
    {
        /// <summary>
        /// Attribute used for marking a string pattern defined in JSON Schemas.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        protected sealed class RegexpPatternAttribute : Attribute
        {
            readonly Regex regexp;

            /// <summary>
            /// Create a new instance of RegexpPatternAttribute
            /// </summary>
            /// <param name="regexp">Regular Expression pattern in string</param>
            public RegexpPatternAttribute(string regexp)
            {
                this.regexp = new Regex(regexp, RegexOptions.Compiled);
            }

            /// <summary>
            /// Returns the compiled regular expression pattern object.
            /// </summary>
            public Regex RegexpPattern
            {
                get { return regexp; }
            }
        }

        /// <summary>
        /// Check if all fields meets the requirements under JSON schema. 
        /// </summary>
        /// <exception cref="ArgumentException">If there is any field fails to be validated.</exception>
        public void Validate()
        {
            foreach (var item in this.GetType().GetProperties())
            {
                var attrs = item.GetCustomAttributes(typeof(JsonPropertyAttribute), true) as JsonPropertyAttribute[];
                if (attrs == null || attrs.Length == 0) continue;

                var regexpAttrs = item.GetCustomAttributes(typeof(RegexpPatternAttribute), true) as RegexpPatternAttribute[];
                if (regexpAttrs == null || regexpAttrs.Length == 0) continue;

                var value = item.GetValue(this, null) as string;
                if (value == null) continue;

                var regexpAttr = regexpAttrs[0];
                if (!regexpAttr.RegexpPattern.IsMatch(value))
                    throw new ArgumentException(String.Format("The field {0} in {1} should follow this pattern {2}", item.Name, this.GetType().Name, regexpAttr.RegexpPattern.ToString()), item.Name);
            }
        }
    }
}
