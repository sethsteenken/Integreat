using System;
using System.Collections.Generic;
using System.Reflection;

namespace Integreat
{
    public abstract class ProcessExecutableAdapter<T> : IProcessExecutableAdapter where T : IExecutable
    {
        protected string GetPropertyValue(PropertyInfo[] properties, dynamic value, string propName)
        {
            return GetPropertyValue(properties, value, propName, true);
        }

        protected string GetPropertyValue(PropertyInfo[] properties, dynamic value, string propName, bool required)
        {
            return GetPropertyValue<string>(properties, value, propName, required);
        }

        protected TPropType GetPropertyValue<TPropType>(PropertyInfo[] properties, dynamic value, string propName)
        {
            return GetPropertyValue<TPropType>(properties, value, propName, true);
        }

        protected TPropType GetPropertyValue<TPropType>(PropertyInfo[] properties, dynamic value, string propName, bool required)
        {
            return PropertyInfoExtensions.GetValue<TPropType>(properties, value, propName, required);
        }

        protected virtual ExecutableConfiguration GetConfiguration(dynamic configurationValues, Type type, PropertyInfo[] properties)
        {
            string exeTypeName;
            int timeout = 0;
            IDictionary<string, string> parameters = new Dictionary<string, string>();

            if (properties.Exists("Type"))
                exeTypeName = configurationValues.Type as string;
            else
                throw new MissingMemberException(type.FullName, "Type");

            exeTypeName = exeTypeName.Replace("Executable", "");

            if (exeTypeName != Type)
                throw new InvalidOperationException($"Supplied Type configuration value '{exeTypeName}' does not match the registered adapter executable type of '{Type}'.");

            if (properties.Exists("Timeout"))
                timeout = (int)((configurationValues.Timeout as int?) ?? 0);

            if (properties.Exists("Parameters"))
                parameters = configurationValues.Parameters as Dictionary<string, string>;

            return new ExecutableConfiguration(exeTypeName, timeout, parameters);
        }

        protected abstract T BuildExecutable(dynamic configurationValues, Type type, PropertyInfo[] properties);

        public virtual string Type => typeof(T).Name.Replace("Executable", "");

        public virtual ProcessExecutable Build(dynamic configurationValues)
        {
            Guard.IsNotNull(configurationValues, nameof(configurationValues));

            Type type = configurationValues.GetType();
            var properties = type.GetProperties();

            var configuration = GetConfiguration(configurationValues, type, properties);
            var executable = BuildExecutable(configurationValues, type, properties);

            return new ProcessExecutable(executable, configuration);
        }
    }
}
