using System;
using System.Collections.Generic;
using Xunit;

namespace Integreat.Core.Tests
{
    public class ExecutableParametersTests
    {
        private static ExecutableParameters _emptyParameters = new ExecutableParameters(new Dictionary<string, string>());

        [Fact]
        public void Get_ThrowsException_WhenKeyIsNull()
        {
            var exception = Record.Exception(() => _emptyParameters.Get(null));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void Get_ThrowsException_WhenKeyIsNotFoundAndRequired()
        {
            var exception = Record.Exception(() => _emptyParameters.Get("missing key", required: true));

            Assert.NotNull(exception);
            Assert.IsType<KeyNotFoundException>(exception);
        }

        [Fact]
        public void Get_ReturnsDefault_WhenKeyIsNotFoundAndNotRequired()
        {
            var stringValue = _emptyParameters.Get("missing key", required: false);
            var intValue = _emptyParameters.Get<int>("missing key", required: false);
            var boolValue = _emptyParameters.Get<bool>("missing key", required: false);
            var guidValue = _emptyParameters.Get<Guid>("missing key", required: false);
           
            Assert.Equal(default(string), stringValue);
            Assert.Equal(default(int), intValue);
            Assert.Equal(default(bool), boolValue);
            Assert.Equal(default(Guid), guidValue);
        }


        [Fact]
        public void Get_ThrowsException_WhenValueNotCorrectFormat()
        {
            var parameters = new ExecutableParameters(new Dictionary<string, string>() { { "mykey", "not_an_integer" } });
            var exception = Record.Exception(() => parameters.Get<int>("mykey"));

            Assert.NotNull(exception);
            Assert.IsType<FormatException>(exception);
        }


        [Fact]
        public void Get_ReturnsValue_WhenKeyIsFound()
        {
            var parameters = new ExecutableParameters(new Dictionary<string, string>() { { "mykey", "my_value" } });
            var value = parameters.Get("mykey");

            Assert.Equal("my_value", value);
        }

        [Fact]
        public void Get_ReturnsParsedIntValue_WhenKeyIsFound()
        {
            var parameters = new ExecutableParameters(new Dictionary<string, string>() { { "mykey", "156" } });
            var value = parameters.Get<int>("mykey");

            Assert.Equal(156, value);
        }

        [Fact]
        public void Get_ReturnsParsedBoolValue_WhenKeyIsFound()
        {
            var parameters = new ExecutableParameters(new Dictionary<string, string>() { { "mykey", "true" } });
            var value = parameters.Get<bool>("mykey");

            Assert.True(value);
        }
    }
}
