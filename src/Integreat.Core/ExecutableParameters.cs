using System;
using System.Collections.Generic;
using System.Linq;

namespace Integreat.Core
{
    public sealed class ExecutableParameters : Dictionary<string, string>
    {
        private ExecutableParameters() { }

        public ExecutableParameters(IDictionary<string, string> parameters)
            : base(parameters)
        {

        }

        public string Get(string key)
        {
            return Get(key, required: true);
        }

        public string Get(string key, bool required)
        {
            return Get<string>(key, required);
        }

        public T Get<T>(string key)
        {
            return Get<T>(key, required: true);
        }

        public T Get<T>(string key, bool required)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            if (!TryGetValue(key, out string value))
            {
                if (required)
                    throw new KeyNotFoundException($"Required parameter '{key}' was not found.");
                else
                    return default(T);
            }

            if (!ValueParser.TryParse<T>(value, out T parsedValue))
                throw new FormatException($"Could not parse parameter '{key}' value '{value}' to type {typeof(T).FullName}.");

            return parsedValue;
        }

        public override string ToString()
        {
            if (Count <= 0)
                return string.Empty;

            return string.Join(",", this.Select(p => $"{p.Key}={p.Value}")).Trim();
        }

        public static readonly ExecutableParameters Empty = new ExecutableParameters();
    }
}
