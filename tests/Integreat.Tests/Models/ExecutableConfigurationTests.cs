using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Integreat.Tests.Models
{
    public class ExecutableConfigurationTests
    {
        [Fact]
        public void Parameters_ReturnEmpty_WhenNullParametersSupplied()
        {
            var config = new ExecutableConfiguration("type", timeout: 30, parameters: null);

            Assert.NotNull(config.Parameters);
        }

        [Fact]
        public void Parameters_ReturnSuppliedParameters_WhenValidParametersSupplied()
        {
            var parameters = new Dictionary<string, string>();
            var config = new ExecutableConfiguration("type", timeout: 30, parameters: parameters);

            Assert.Equal(parameters, config.Parameters);
        }

        [Theory]
        [InlineData("this_type")]
        [InlineData("that_type")]
        public void Type_ReturnSuppliedType_WhenValidTypeSupplied(string type)
        {
            var config = new ExecutableConfiguration(type, timeout: 30, parameters: null);

            Assert.Equal(type, config.Type);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(25)]
        [InlineData(-7)]
        public void Timeout_ReturnSuppliedTimeout_WhenValidTimeoutSupplied(int timeout)
        {
            var config = new ExecutableConfiguration("my_type", timeout: timeout, parameters: null);

            Assert.Equal(timeout, config.Timeout);
        }
    }
}
