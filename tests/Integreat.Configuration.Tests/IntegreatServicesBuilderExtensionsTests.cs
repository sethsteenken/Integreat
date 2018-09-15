using System;
using Xunit;

namespace Integreat.Configuration.Tests
{
    public class IntegreatServicesBuilderExtensionsTests
    {
        [Fact]
        public void AddJsonConfigurationSettings_ThrowsException_WhenBuilderIsNull()
        {
            var exception = Record.Exception(() => IntegreatServicesBuilderExtensions.AddJsonConfigurationSettings(null));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}
