using System;

namespace Integreat.Core
{
    public class StringExecutableParameter : ExecutableParameter
    {
        protected StringExecutableParameter(string name, object value) 
            : base(name, value)
        {
        }

        public override string ToParamString()
        {
            throw new NotImplementedException();
        }
    }
}
