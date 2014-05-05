using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace RippleRest
{
    /// <summary>
    /// Serializable Expandable Object Converter
    /// 
    /// (http://stackoverflow.com/questions/17560085/problems-using-json-net-with-expandableobjectconverter)
    /// </summary>
    public class SerializableExpandableObjectConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// CanConvertFrom
        /// </summary>
        /// <param name="context">ITypeDescriptorContext</param>
        /// <param name="sourceType">Type</param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return false;
            return base.CanConvertTo(context, sourceType);
        }

        /// <summary>
        /// CanConvertTo
        /// </summary>
        /// <param name="context">ITypeDescriptorContext</param>
        /// <param name="destinationType">Type</param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string)) return false;
            return base.CanConvertTo(context, destinationType);
        }
    }
}
