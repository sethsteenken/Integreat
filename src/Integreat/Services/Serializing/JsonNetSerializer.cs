using System;
using System.Collections.Generic;
using System.Text;

namespace Integreat
{
    public class JsonNetSerializer : ISerializer
    {
        public T Deserialize<T>(string serializedValue) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
