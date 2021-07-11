using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Queue.Utils
{
    public static class ExtensionMethods
    {
        public static SelectList ToSelectList<TEnum>(this TEnum obj, object selectedValue)
    where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            if (selectedValue != null)
            {
                return new SelectList(Enum.GetValues(typeof(TEnum))
            .OfType<Enum>()
            .Select(x => new SelectListItem
            {
                Text = GetDisplayValue(x),
                Value = (Convert.ToInt32(x))
                .ToString()
            }), "Value", "Text", selectedValue);
            }
            else {
                return new SelectList(Enum.GetValues(typeof(TEnum))
            .OfType<Enum>()
            .Select(x => new SelectListItem
            {
                Text = GetDisplayValue(x),
                Value = (Convert.ToInt32(x))
                .ToString()
            }), "Value", "Text");
            }
            
        }

        public static string GetDisplayValue(object value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes.Length > 0)
            {
                if (descriptionAttributes[0].ResourceType != null)
                    return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);
            }

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }
        private static string lookupResource(Type resourceManagerProvider, string resourceKey)
        {
            var resourceKeyProperty = resourceManagerProvider.GetProperty(resourceKey,
                BindingFlags.Static | BindingFlags.Public, null, typeof(string),
                new Type[0], null);
            if (resourceKeyProperty != null)
            {
                return (string)resourceKeyProperty.GetMethod.Invoke(null, null);
            }

            return resourceKey; // Fallback with the key name
        }
    }
}