using System;

namespace Integreat.Core
{
    public abstract class ExecutableParameter
    {
        protected ExecutableParameter(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name.Trim();
            Value = value;
        }

        public string Name { get; private set; }
        public object Value { get; private set; }

        public abstract string ToParamString();

        public override string ToString()
        {
            return ToParamString();
        }
    }
}
