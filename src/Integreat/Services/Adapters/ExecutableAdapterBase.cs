using System;
using System.Collections.Generic;
using System.Reflection;

namespace Integreat
{
    public abstract class ExecutableAdapterBase
    {
        protected string GetPropertyValue(PropertyInfo[] properties, dynamic value, string propName)
        {
            return GetPropertyValue(properties, value, propName, true);
        }

        protected string GetPropertyValue(PropertyInfo[] properties, dynamic value, string propName, bool required)
        {
            return GetPropertyValue<string>(properties, value, propName, required);
        }

        protected T GetPropertyValue<T>(PropertyInfo[] properties, dynamic value, string propName)
        {
            return GetPropertyValue<T>(properties, value, propName, true);
        }

        protected T GetPropertyValue<T>(PropertyInfo[] properties, dynamic value, string propName, bool required)
        {
            return PropertyInfoExtensions.GetValue<T>(properties, value, propName, required);
        }

        protected ExecutableConfiguration GetConfiguration(dynamic configurationValues)
        {
            return GetConfiguration(configurationValues, (Type)configurationValues.GetType());
        }

        protected ExecutableConfiguration GetConfiguration(dynamic configurationValues, Type type)
        {
            return GetConfiguration(configurationValues, type, type?.GetProperties());
        }

        protected ExecutableConfiguration GetConfiguration(dynamic configurationValues, Type type, PropertyInfo[] properties)
        {
            string exeTypeName;
            int timeout = 0;
            IDictionary<string, string> parameters = new Dictionary<string, string>();

            if (properties.Exists("Type"))
                exeTypeName = configurationValues.Type as string;
            else
                throw new MissingMemberException(type.FullName, "Type");

            if (properties.Exists("Timeout"))
                timeout = (int)((configurationValues.Timeout as int?) ?? 0);

            if (properties.Exists("Parameters"))
                parameters = configurationValues.Parameters as Dictionary<string, string>;

            return new ExecutableConfiguration(exeTypeName, timeout, parameters);
        }
    }
}
