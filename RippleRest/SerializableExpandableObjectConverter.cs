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
        /// <inheritdoc/>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return false;
            return base.CanConvertTo(context, sourceType);
        }

        /// <inheritdoc/>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string)) return false;
            return base.CanConvertTo(context, destinationType);
        }
    }
}
