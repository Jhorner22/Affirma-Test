using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CarDealership.Helpers
{
    /// <summary>
    /// Extensions for Lookup enumerations.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieves the DisplayName attribute value on the given enumeration value.
        /// </summary>
        public static string GetDisplayName(this Enum GenericEnum)
        {
            var fieldInfo = GenericEnum.GetType().GetField(GenericEnum.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : GenericEnum.ToString();
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

    public static class ExtensionMethods
    {

    }
}
