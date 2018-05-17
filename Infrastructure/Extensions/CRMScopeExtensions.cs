using System; 
using Microsoft.Xrm.Sdk;
namespace Infrastructure.Extensions
{

    public static class CRMScopeExtensions
    {
        public static T GetAliasedAttributeValue<T>(this Entity entity, string attributeName)
        {
            if (entity == null)
                return default(T);

            var fieldAliasValue = entity.GetAttributeValue<AliasedValue>(attributeName);

            return fieldAliasValue == null
                ? default(T)
                : (fieldAliasValue.Value != null && fieldAliasValue.Value.GetType() == typeof(T)
                    ? (T)fieldAliasValue.Value
                    : default(T));
        }

        public static T GetCrmAttributeValue<T>(this Entity entity, string attributeName)
        {
            if (entity == null)
                return default(T);

            var attributeValue = entity.GetAttributeValue<T>(attributeName);

            return attributeValue == null
               ? default(T)
               : (attributeValue != null && attributeValue.GetType() == typeof(T) ? (T)attributeValue : default(T));
        }
        public static DateTime GetCrmDateAttributeValue<T>(this Entity entity, string attributeName)
        {
            if (entity == null) return new DateTime();

            var attributeValue = entity.GetAttributeValue<T>(attributeName);

            return attributeValue == null
                ? new DateTime()
                : (attributeValue != null && attributeValue.GetType() == typeof(T)
                    ? Convert.ToDateTime(attributeValue)
                    : new DateTime());
        }
        public static string GetCrmAttributeValue<T>(this Entity entity, string attributeName, int value)
        {
            if (entity == null) return "";
            var attributeValue = entity.GetAttributeValue<string>(attributeName);

            return attributeValue == null
                ? ""
                : (attributeValue != null && attributeValue.GetType() == typeof(T)
                    ? attributeValue.Substring(attributeValue.Length - value, value)
                    : "");
        }
    }
    
}
