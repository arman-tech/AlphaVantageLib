using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AlphaVantage.Common
{
    public static class AttributeHelper
    {
        public static TValue GetPropertyAttributeValue<T, TOut, TAttribute, TValue>(
            Expression<Func<T, TOut>> propertyExpression,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var expression = (MemberExpression)propertyExpression.Body;
            var propertyInfo = (PropertyInfo)expression.Member;
            var attr = propertyInfo.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            return attr != null ? valueSelector(attr) : default(TValue);
        }

        public static TOut GetPropertyBasedOnAvPropertyName<T, TOut, TAttribute, TValue>(
            string attributeName, T obj,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : AvPropertyNameAttribute

        {
            // sanity check - if valueSelect is null then throw an exception
            if (valueSelector == null)
                throw new ArgumentNullException(nameof(valueSelector));

            // get properties where it has the custom attribute we are looking for with the property name in question.
            // also ensure that the property type matches the TOut type mentioned.
            var propInfo = typeof(T).GetProperties()
                .FirstOrDefault(x => x.GetCustomAttribute<TAttribute>(true) != null
                   && x.GetCustomAttribute<TAttribute>(true).ExtractPropertyName.Equals(attributeName)
                   && x.PropertyType == typeof(TOut));


            return propInfo != null ? (TOut)propInfo.GetValue(obj) : default(TOut);

        }

        public static void SetPropertyBasedOnAvPropertyName<T, TKind, TAttribute, TValue>(
            string attributeName, T obj, TKind val,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : AvPropertyNameAttribute
            where TValue : class
        {
            if (valueSelector == null)
                throw new ArgumentNullException(nameof(valueSelector));

            var propInfo = typeof(T).GetProperties()
                .FirstOrDefault(x => x.GetCustomAttribute<TAttribute>(true) != null
                   && x.GetCustomAttribute<TAttribute>(true).ExtractPropertyName.Equals(attributeName)
                   && x.PropertyType == typeof(TKind));

            if (null != propInfo)
            {
                propInfo.SetValue(obj, val);
            }

        }
    }
}
