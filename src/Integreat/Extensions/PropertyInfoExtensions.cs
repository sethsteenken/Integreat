using System;
using System.Linq;
using System.Reflection;

namespace Integreat
{
    public static class PropertyInfoExtensions
    {
        public static bool Exists(this PropertyInfo[] properties, string name)
        {
            return properties?.Any(p => p.Name.Equals(name)) ?? false;
        }

        public static T GetValue<T>(this PropertyInfo[] properties, dynamic dynamicItem, string propName, bool required = true)
        {
            if (Exists(properties, propName))
            {
                return (T)properties.FirstOrDefault(p => p.Name.Equals(propName)).GetValue(dynamicItem as object);
            }
            else if (required)
            {
                throw new MissingMemberException($"Required property '{propName}' missing.");
            }
            else
            {
                return default(T);
            }
        }
    }
}
